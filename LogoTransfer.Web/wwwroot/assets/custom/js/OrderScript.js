var isRun = false;
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
        columnDefs: [
            { "className": "dt-center", "targets": "_all" },
            { "width": "20%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "30%", "targets": 4 },
            { "width": "15%", "targets": 5 },
            { "width": "2%", "targets": [0, 6, 7, 8, 9, 10] }
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
            {
                data: 'date_',
                "render": function (data, type, full, meta) {
                    return full.date_.split('T')[0];
                }
            },
            { data: 'currTransaction' },
            { data: 'amount' },
            { data: 'status' },
            { data: 'integration' },
            {
                data: 'transferStatus',
                "render": function (data, type, full, meta) {
                    if (!full.transferStatus) {
                        return '<i style="font-size:30px;"class="zmdi zmdi-thumb-down col-red"></i>';
                    }
                    return '<i style="font-size:30px;"class="zmdi zmdi-thumb-up col-green"></i>';
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
        "lengthChange": false,
        pageLength: 30
    });
}

function openOrderDetailModal(id) {
    $('#orderDetailModal').modal('show');
    getOrderDetails(id);
}

function getOrderDetails(id) {
    var table = $('#orderDetailTable').removeAttr('width');
    table.DataTable().destroy();
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/order/GetTransactionsByOrderId/' + id,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        },
        columnDefs: [
            { "className": "dt-center", "targets": "_all" },
            { "width": "30%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "5%", "targets": [3, 4, 5, 6, 7, 8, 9, 10] }
        ],
        columns: [
            { data: 'name' },
            { data: 'otherCode' },
            { data: 'masterCode' },
            {
                data: 'quantity'
            },
            { data: 'unitCode' },
            {
                data: 'price'
            },
            {
                data: 'vatRate'
            },
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
                    if (full.isProductMatch) {
                        return `<button type="button" class="btn btn-primary disabled">Eşleştir</button>`;
                    }
                    return `<button type="button" class="btn btn-primary" onclick="openMasterProductListModal('` + full.otherCode + `')">Eşleştir</button>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },
        processing: true
    });
}

async function openMasterProductListModal(otherCode) {
    $('#masterProductListModal #otherCode').val(otherCode);
    if (!isRun) {
        $(".page-loader-wrapper").show();
        await getMasterProducts();
        setTimeout(function () {
            $(".page-loader-wrapper").hide();
        }, 1000);
    }
    $('#masterProductListModal').modal('show');
}

async function getMasterProducts() {
    isRun = true;
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
            { "className": "dt-center", "targets": "_all" },
            { "width": "50%", "targets": 0 },
        ],
        columns: [
            {
                data: 'otherCode'
            },
            {
                data: 'code'
            },
            {
                data: 'process',
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<a class="btn btn-success" onclick="productMatch('` + full.code + `')">Seç</a>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },
        "paging": true,
        "info": false,
        processing: true
    });
    return;
}

$("body").on('click', '#orderTable tbody tr', function () {
    $(this).toggleClass("selected");
})

function startThisTransfer(id) {
    var allData = $('#orderTable').DataTable().rows().data().toArray();
    let postData = [];
    var selectedData = allData.filter(x => x.transferStatus == false && x.id == id);
    var checkedData = selectedData.filter(x => x.transactions.every(y => y.masterCode != ""));

    if (selectedData.length != 0) {

        if (checkedData.length != 0) {
            let orderData = {};
            orderData.number = checkedData[0].number;
            orderData.date_ = checkedData[0].date_;
            orderData.auxilCode = checkedData[0].auxilCode;
            orderData.email = checkedData[0].email;
            orderData.phoneNumber = checkedData[0].phoneNumber;
            orderData.customerName = checkedData[0].customerName;
            orderData.customerSurName = checkedData[0].customerSurName;
            orderData.rcXrate = checkedData[0].rcXrate;
            orderData.currTransaction = checkedData[0].currTransaction;
            orderData.tcXrate = checkedData[0].tcXrate;
            orderData.taxNo = checkedData[0].taxNo;
            orderData.taxOffice = checkedData[0].taxOffice;
            orderData.tckNumber = checkedData[0].tckNumber; 
            orderData.town = checkedData[0].town; 
            orderData.city = checkedData[0].city; 
            orderData.address = checkedData[0].address; 
            orderData.transactions = [];

            for (var i = 0; i < checkedData[0].transactions.length; i++) {
                let orderTransactionData = {};
                orderTransactionData.masterCode = checkedData[0].transactions[i].masterCode;
                orderTransactionData.quantity = checkedData[0].transactions[i].quantity;
                orderTransactionData.price = checkedData[0].transactions[i].price;
                orderTransactionData.transDescription = checkedData[0].transactions[i].transDescription;
                orderTransactionData.unitCode = checkedData[0].transactions[i].unitCode;
                orderTransactionData.unitConv1 = checkedData[0].transactions[i].unitConv1;
                orderTransactionData.unitConv2 = checkedData[0].transactions[i].unitConv2;
                orderTransactionData.currTrans = checkedData[0].transactions[i].currTrans;
                orderTransactionData.tcXrate = checkedData[0].transactions[i].tcXrate;
                orderTransactionData.vatRate = checkedData[0].transactions[i].vatRate;
                orderData.transactions.push(orderTransactionData);
            }

            postData.push(orderData);
            var json = JSON.stringify(postData);

            $(".page-loader-wrapper").show();
            $.ajax({
                url: baseApiUrl + '/order/orderImport',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                data: json,
                success: function (data) {
                    $(".page-loader-wrapper").hide();
                    if (data.data[0].returnError == "") {
                        swal.fire('Tebrikler!', 'Sipariş Aktarıldı.', 'success');
                        $('#orderTable').DataTable().ajax.reload();
                    }
                    else {
                        swal.fire('Hata!', data.data[0].returnError, 'error');
                    }
                },
                error: function () {
                    $(".page-loader-wrapper").hide();
                    swal.fire('Hata!', 'Bir Hata Oluştu.', 'error');
                }
            });
        }
        else {
            swal.fire('Hata!', 'Siparişe ait eşleşmeyen ürün kodları var.', 'error');
        }
    }
    else {
        swal.fire('Uyarı!', 'Sipariş zaten aktarılmış.', 'info');
    }
}

function productMatch(masterCode) {
    otherCode = $('#masterProductListModal #otherCode').val();
    $(".page-loader-wrapper").show();
    $.ajax({
        url: baseApiUrl + '/product/GetProductByCode/' + masterCode,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            if (data.data.otherCode != null) {
                $(".page-loader-wrapper").hide();
                Swal.fire({
                    title: 'Emin misiniz?',
                    text: "Seçmiş olduğunuz kod daha önce '" + data.data.otherCode + "' kodu ile eşleştirilmiş. Tüm siparişlerdeki ilgili ürün kodu güncellenecek.",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Devam Et!',
                    cancelButtonText: 'İptal Et'
                }).then((result) => {
                    if (result.isConfirmed) {
                        productAcceptMatch(masterCode, otherCode);
                        $('#orderTable').DataTable().ajax.reload();
                    }
                })
            }
            else {
                productAcceptMatch(masterCode, otherCode);
                $('#orderTable').DataTable().ajax.reload();
            }
        },
        error: function () {
            $(".page-loader-wrapper").hide();
        }
    });
}

function productAcceptMatch(masterCode, otherCode) {
    $(".page-loader-wrapper").show();
    let productMathingData = {
        code: masterCode,
        otherCode: otherCode
    };

    var json = JSON.stringify(productMathingData);

    $(".page-loader-wrapper").show();
    $.ajax({
        url: baseApiUrl + '/product/match',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: json,
        success: function (data) {
            $(".page-loader-wrapper").hide();
            $('#orderDetailTable').DataTable().ajax.reload();
            $('#masterProductListModal').modal('hide');
            swal.fire('Tebrikler!', 'Ürünler Eşleştirildi.', 'success');
            $.ajax({
                url: location.origin + '/Order/UpdateMasterProductsInCache',
                type: 'GET',
                contentType: 'application/json; charset=utf-8'
                //success: function () {
                //    isRun = false;
                //}
            });
        },
        error: function () {
            $(".page-loader-wrapper").hide();
        }
    });
}