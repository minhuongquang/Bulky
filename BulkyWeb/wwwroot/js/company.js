var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData ').DataTable({
        "ajax": { url: '/admin/company/getall' },
        "columns": [
            { data: 'id', "with": "5%" },
            { data: 'name', "with": "15%" },
            { data: 'streetAddress', "with": "10%" },
            { data: 'city', "with": "10%" },
            { data: 'state', "with": "10%" },
            { data: 'postalCode', "with": "10%" },
            { data: 'phoneNumber', "with": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group role="group">
                                <a href="/admin/company/upsert?id=${data}" class="btn btn-primary mx-2">Edit <i class="bi bi-pencil-square"></i></a>
                                <a onClick=Delete('/admin/company/delete/${data}') class="btn btn-danger mx-2">Delete <i class="bi bi-trash"></i></a>
                    </div>`
                },
                "with": "25%"
            }

        ]

    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })

}
