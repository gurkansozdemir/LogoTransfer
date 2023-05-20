function getOrders() {
    var table = $('#orderTable');
    table.DataTable().destroy();
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/order',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        },
        columns: [
            { data: 'storeName' },
            { data: 'number' },
            { data: 'customerName' },
            { data: 'customerSurName' },
            { data: 'email' },
            { data: 'phoneNumber' },
            { data: 'date_', },
            { data: 'currTransaction' },
            { data: 'amount' },
            { data: 'integration' },
            {
                data: 'transferStatus',
                "render": function (data, type, full, meta) {
                    return '<span class="badge badge-success">Tamamlandı</span>';
                }
            },
            {
                data: 'process',
                "render": function (data, type, full, meta) {
                    return `<button type="button" class="btn btn-warning btn-sm" onclick="openOrderDetailModal('` + full.id + `')"><i class="zmdi"></i></button>
                            <button class="btn btn-success btn-sm"><i class="zmdi"></i></button>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
};

function openOrderDetailModal(id) {
    getOrderDetails(id);
    $('#orderDetailModal').modal('show');
}

function getOrderDetails(id) {
    var table = $('#orderDetailTable');
    table.DataTable().destroy();
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/order/GetTransactionsByOrderId/' + id,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        },
        columns: [
            { data: 'name' },
            { data: 'code' },
            { data: 'quantity' },
            { data: 'price' },
            { data: 'priceRatio' },
            {
                data: 'totalAmount',
                "render": function (data, type, full, meta) {
                    return `0`;
                }
            },
            { data: 'currency' },
            {
                data: 'mathing',
                "render": function (data, type, full, meta) {
                    if (full.isMatch) {
                        return '<span class="badge badge-success">Eşleşdi</span>';
                    }
                    return '<span class="badge badge-danger">Eşleşmedi</span>';
                }
            },
            {
                data: 'process',
                "render": function (data, type, full, meta) {
                    return `<button class="btn btn-success btn-sm"><i class="zmdi"></i></button>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
};

$("body").on('click', '#orderTable tbody tr', function () {
    $(this).toggleClass("selected");
});

function startTransfer() {
    $('#orderTable').DataTable().rows('.selected').data();
}