var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData ').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "with": "15%" },
            { data: 'email', "with": "15%" },
            { data: 'phoneNumber', "with": "15%" },
            { data: 'company.name', "with": "15%" },
            { data: 'role', "with": "15%" },
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
