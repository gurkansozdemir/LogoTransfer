function getOrders() {
    var table = $('#orderTable');
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/Order',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            data: {
                pagination: {
                    perpage: 50,
                },
            },
        },
        columns: [
            {
                data: 'store',
                "render": function (data, type, full, meta) {
                    return 'IdeaSoft';
                }
            },
            {
                data: 'orderNo',
                "render": function (data, type, full, meta) {
                    debugger;
                    return full['transactionId'];
                }
            },
            {
                data: 'customer',
                "render": function (data, type, full, meta) {
                    return '<a>' + full['customerFirstname'] + ' ' + full['customerSurname'] + '</a>';
                }
            },
            {
                data: 'date',
                "render": function (data, type, full, meta) {
                    return full['createdAt'];
                }
            },
            { data: 'currency' },
            {
                data: 'Amount',
                "render": function (data, type, full, meta) {
                    return full['finalAmount'];
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
};