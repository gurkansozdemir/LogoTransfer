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
        select: {
            style: 'multi'
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        columns: [
            { data: 'storeName' },
            { data: 'number' },
            {
                data: 'customer',
                "render": function (data, type, full, meta) {
                    return full.customerName + " " + full.customerSurName;
                }
            },
            {
                data: 'email',
                orderable: false,
            },
            {
                data: 'phoneNumber',
                orderable: false,
            },
            { data: 'date_', },
            { data: 'currTransaction' },
            { data: 'amount' },
            {
                data: 'integration',
                "render": function (data, type, full, meta) {
                    return '<i style="font-size:30px;"class="zmdi zmdi-thumb-down col-red"></i>';
                }
            },
            {
                data: 'process',
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<div class="dropdown show">
                              <a class="btn btn-secondary dropdown-toggle" href="javascript:void()" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                İşlemler
                              </a>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                <a class="dropdown-item" href="javascript:void()" onclick="openOrderDetailModal('` + full.id + `')">Sipariş Detayı</a>
                                <a class="dropdown-item" href="javascript:void()">Siparişi Aktar</a>
                              </div>
                            </div>`;
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
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        columns: [
            { data: 'name' },
            { data: 'otherCode' },
            { data: 'masterCode' },
            { data: 'transDescription' },
            { data: 'quantity' },
            { data: 'unitCode' },
            { data: 'price' },
            { data: 'vatRate' },
            {
                data: 'totalAmount',
                "render": function (data, type, full, meta) {
                    return `0`;
                }
            },
            { data: 'currTrans' },
            {
                data: 'mathing',
                "render": function (data, type, full, meta) {
                    if (full.isProductMatch) {
                        return '<span class="badge badge-success">Eşleşdi</span>';
                    }
                    return '<span class="badge badge-danger">Eşleşmedi</span>';
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

//function startSelectedTransfer() {
//    postData = $('#orderTable').DataTable().rows('.selected').data().toArray();
//    var myJsonString = JSON.stringify(postData);
//    $.ajax({
//        url: baseApiUrl + '/order/orderImport',
//        type: 'POST',
//        contentType: 'application/json; charset=utf-8',
//        dataType: "json",
//        data: myJsonString,
//        success: function () {

//        },
//        error: function () {

//        }
//    });
//}

function startTransfer() {

    let data = {
        number: "",
        date_: "",
        auxilCode: "",
        email: "",
        phoneNumber: "",
        customerName: "",
        customerSurName: "",
        rcXrate: 0,
        currTransaction: "",
        tcXrate: 0,
        transactions: []
    };

    var postData = [];
    var allData = $('#orderTable').DataTable().rows().data().toArray();
    for (var i = 0; i < allData.length; i++) {
        data.number = allData[i].number;
        data.date_ = allData[i].date_;
        data.auxilCode = allData[i].auxilCode;
        data.email = allData[i].email;
        data.phoneNumber = allData[i].phoneNumber;
        data.customerName = allData[i].customerName;
        data.customerSurName = allData[i].customerSurName;
        data.rcXrate = allData[i].rcXrate;
        data.currTransaction = allData[i].currTransaction;
        data.tcXrate = allData[i].tcXrate;
        data.transactions = allData[i].transactions;

        postData.push(data);
    }
    var json = JSON.stringify(postData);

    $.ajax({
        url: baseApiUrl + '/order/orderImport',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: json,
        success: function (data) {
            console.log(data.data.ReturnNumber);
            console.log(data.data.returnError);
        },
        error: function () {

        }
    });
}