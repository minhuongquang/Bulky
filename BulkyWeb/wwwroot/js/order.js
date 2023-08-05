var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData ').DataTable({
        "ajax": { url:'/admin/order/getall' },
        "columns": [
            { data: 'id', "with":"5%" },
            { data: 'name', "with": "15%" },
            { data: 'phoneNumber', "with": "15%" },
            { data: 'applicationUser.email', "with": "15%" },
            { data: 'orderStatus', "with": "10%" },
            { data: 'orderTotal', "with": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group role="group">
                                <a href="/admin/order/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                    </div>`
                },
                "with": "25%"
            }

        ]

    });
}

