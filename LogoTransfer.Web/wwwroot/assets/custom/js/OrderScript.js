function getOrders() {
    var table = $('#orderTable');
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/Order',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        },
        columns: [
            { data: 'storeName' },
            { data: 'orderNo' },
            {
                data: 'customer',
                "render": function (data, type, full, meta) {
                    return '<a>' + full['customerFirstName'] + ' ' + full['customerLastName'] + '</a>';
                }
            },
            {
                data: 'date',
                "render": function (data, type, full, meta) {
                    return full['createdOn'];
                }
            },
            { data: 'currency' },
            {
                data: 'Amount',
                "render": function (data, type, full, meta) {
                    return full['amount'];
                }
            },
            {
                data: 'transferStatus',
                "render": function (data, type, full, meta) {
                    return '<span class="badge badge-success">Tamamlandı</span>';
                }
            },
            { data: 'integration' },
            {
                data: 'process',
                "render": function (data, type, full, meta) {
                    return `<button class="btn btn-warning btn-sm"><i class="zmdi"></i></button>
                            <button class="btn btn-success btn-sm"><i class="zmdi"></i></button>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
};