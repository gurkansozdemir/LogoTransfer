function getOrders() {
    var table = $('#orderTable');
    table.DataTable().destroy();
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/order/getAllWithTransactions',
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
                                <a class="dropdown-item ` + (full.transferStatus == true ? "disabled" : "") + `" href="javascript:void()" onclick="startThisTransfer('` + full.id + `')">Siparişi Aktar</a>
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
}

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
                        return '<i style="font-size:30px;"class="zmdi zmdi-thumb-up col-green"></i>';
                    }
                    return '<i style="font-size:30px;"class="zmdi zmdi-thumb-down col-red"></i>';
                }
            },
            {
                data: 'process',
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<button type="button" class="btn btn-primary" onclick="openMasterProductListModal('` + full.otherCode + `')">Eşleştir</button>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
}

function openMasterProductListModal(otherCode) {
    getMasterProducts(otherCode);
    $('#masterProductListModal').modal('show');
}

function getMasterProducts(otherCode) {
    var table = $('#masterProductTable');
    table.DataTable().destroy();
    table.DataTable({
        ajax: {
            url: 'getMasterProducts',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": [1] }
        ],
        columns: [
            {
                data: 'code',
                data: 'otherCode',
                orderable: false,
            },
            {
                data: 'process',
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<a class="btn btn-success" onclick="productMatch('` + full.code + `','` + otherCode + `')">Seç</a>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },
        "paging": true,
        "ordering": false,
        "info": false
    });
}

$("body").on('click', '#orderTable tbody tr', function () {
    $(this).toggleClass("selected");
});

let orderData = {
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

let orderTransactionData = {
    masterCode: "",
    quantity: 0,
    price: 0,
    transDescripntion: "",
    unitCode: "",
    unitConv1: 0,
    unitConv2: 0,
    currTrans: "",
    tcXrate: 0,
    vatRate: 0
};

function startThisTransfer(id) {
    var allData = $('#orderTable').DataTable().rows().data().toArray();
    var postData = [];
    var selectedData = allData.filter(x => x.transferStatus == false && x.id == id);

    if (selectedData.length != 0) {
        orderData.number = selectedData[0].number;
        orderData.date_ = selectedData[0].date_;
        orderData.auxilCode = selectedData[0].auxilCode;
        orderData.email = selectedData[0].email;
        orderData.phoneNumber = selectedData[0].phoneNumber;
        orderData.customerName = selectedData[0].customerName;
        orderData.customerSurName = selectedData[0].customerSurName;
        orderData.rcXrate = selectedData[0].rcXrate;
        orderData.currTransaction = selectedData[0].currTransaction;
        orderData.tcXrate = selectedData[0].tcXrate;

        for (var i = 0; i < selectedData[0].transactions.length; i++) {
            orderTransactionData.masterCode = selectedData[0].transactions[i].masterCode;
            orderTransactionData.quantity = selectedData[0].transactions[i].quantity;
            orderTransactionData.price = selectedData[0].transactions[i].price;
            orderTransactionData.transDescripntion = selectedData[0].transactions[i].transDescription;
            orderTransactionData.unitCode = selectedData[0].transactions[i].unitCode;
            orderTransactionData.unitConv1 = selectedData[0].transactions[i].unitConv1;
            orderTransactionData.unitConv2 = selectedData[0].transactions[i].unitConv2;
            orderTransactionData.currTrans = selectedData[0].transactions[i].currTrans;
            orderTransactionData.tcXrate = selectedData[0].transactions[i].tcXrate;
            orderTransactionData.vatRate = selectedData[0].transactions[i].vatRate;
            orderData.transactions.push(orderTransactionData);
        }

        postData.push(orderData);
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
                $('#orderTable').DataTable().ajax.reload();
            },
            error: function () {

            }
        });
    }
    else {
        alert("Sipariş zaten aktarılmış.");
    }
}

function productMatch(masterCode, otherCode) {
    let productMathingData = {
        code: masterCode,
        otherCode: otherCode
    };

    var json = JSON.stringify(productMathingData);

    $.ajax({
        url: baseApiUrl + '/product/match',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: json,
        success: function (data) {
            $('#orderDetailTable').DataTable().ajax.reload();
            swal.fire('Tebrikler!','Ürünler Eşleştirildi.','success')
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

//function startAllTransfer() {
//    var allData = $('#orderTable').DataTable().rows().data().toArray();
//    allData = allData.filter(x => x.transferStatus == false);
//    var postData = [];
//    for (var i = 0; i < allData.length; i++) {
//        data.number = allData[i].number;
//        data.date_ = allData[i].date_;
//        data.auxilCode = allData[i].auxilCode;
//        data.email = allData[i].email;
//        data.phoneNumber = allData[i].phoneNumber;
//        data.customerName = allData[i].customerName;
//        data.customerSurName = allData[i].customerSurName;
//        data.rcXrate = allData[i].rcXrate;
//        data.currTransaction = allData[i].currTransaction;
//        data.tcXrate = allData[i].tcXrate;
//        data.transactions = allData[i].transactions;

//        postData.push(data);
//    }
//    var json = JSON.stringify(postData);

//    $.ajax({
//        url: baseApiUrl + '/order/orderImport',
//        type: 'POST',
//        contentType: 'application/json; charset=utf-8',
//        dataType: "json",
//        data: json,
//        success: function (data) {
//            alert(data.data[0].returnNumber);
//            console.log(data.data.returnError);
//            $('#orderTable').DataTable().ajax.reload();
//        },
//        error: function () {

//        }
//    });
//}