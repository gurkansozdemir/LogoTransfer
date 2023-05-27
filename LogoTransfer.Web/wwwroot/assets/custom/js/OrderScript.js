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
                    if (full.integration == "") {
                        return '<i style="font-size:30px;"class="zmdi zmdi-thumb-down col-red"></i>';
                    }
                    return full.integration;
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
                                <a class="dropdown-item" href="javascript:void()" onclick="startThisTransfer('` + full.id + `')">Siparişi Aktar</a>
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

function startAllTransfer() {
    var allData = $('#orderTable').DataTable().rows().data().toArray();
    allData = allData.filter(x => x.transferStatus == false);
    var postData = [];
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
            alert(data.data[0].returnNumber);
            console.log(data.data.returnError);
            $('#orderTable').DataTable().ajax.reload();
        },
        error: function () {

        }
    });
}

function startThisTransfer(id) {
    var allData = $('#orderTable').DataTable().rows().data().toArray();
    var postData = [];
    var selectedData = allData.filter(x => x.transferStatus == false && x.id == id);

    data.number = selectedData.number;
    data.date_ = selectedData.date_;
    data.auxilCode = selectedData.auxilCode;
    data.email = selectedData.email;
    data.phoneNumber = selectedData.phoneNumber;
    data.customerName = selectedData.customerName;
    data.customerSurName = selectedData.customerSurName;
    data.rcXrate = selectedData.rcXrate;
    data.currTransaction = selectedData.currTransaction;
    data.tcXrate = selectedData.tcXrate;
    data.transactions = selectedData.transactions;

    postData.push(data);
    var json = JSON.stringify(postData);

    $.ajax({
        url: baseApiUrl + '/order/orderImport',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: json,
        success: function (data) {
            alert(data.data[0].returnNumber);
            console.log(data.data[0].returnError);
        },
        error: function () {

        }
    });
}

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