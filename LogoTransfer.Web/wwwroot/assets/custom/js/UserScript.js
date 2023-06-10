function getUsers() {
    var table = $('#userTable');
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/user/AllWithRole',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        },
        columnDefs: [
            { "className": "dt-center", "targets": "_all" }
        ],
        columns: [
            { data: 'firstName' },
            { data: 'lastName' },
            {
                data: 'userName',
                orderable: false
            },
            {
                data: 'eMail',
                orderable: false
            },
            {
                data: 'password',
                orderable: false
            },
            {
                data: 'role',
                orderable: false,
                "render": function (data, type, full, meta) {
                    return full.role.description;
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
                                <a class="dropdown-item" href="javascript:void()" onclick="deleteUser('` + full.id + `')">Sil</a>
                                <a class="dropdown-item" href="javascript:void()" onclick="updateUser('` + full.id + `')">Güncelle</a>  
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
};

function deleteUser(id) {
    $.ajax({
        url: baseApiUrl + '/user/' + id,
        type: 'DELETE',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#userTable').DataTable().ajax.reload();
            swal.fire('Tebrikler!', 'Kullanıcı Silindi.', 'success');
        },
        error: function () {

        }
    });
}
function updateUser(id) {
    $.ajax({
        url: baseApiUrl + '/user/' + id,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#updateUserFormModal #updateUserForm #id').val(data.data.id);
            $('#updateUserFormModal #updateUserForm #firstName').val(data.data.firstName);
            $('#updateUserFormModal #updateUserForm #lastName').val(data.data.lastName);
            $('#updateUserFormModal #updateUserForm #userName').val(data.data.userName);
            $('#updateUserFormModal #updateUserForm #eMail').val(data.data.eMail);
            $('#updateUserFormModal #updateUserForm #password').val(data.data.password);
            document.getElementById('roleId').value = data.data.roleId;
            $('#updateUserFormModal').modal('show');
        },
        error: function () {

        }
    });
}

$("#createUserForm").on("submit", function (event) {
    event.preventDefault();
    $(".page-loader-wrapper").show();
    var data = convertFormToJSON($(this));
    var json = JSON.stringify(data);

    $.ajax({
        url: baseApiUrl + '/user',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: json,
        success: function (data) {
            $(".page-loader-wrapper").hide();
            $('#userTable').DataTable().ajax.reload();
            $('#createUserFormModal').modal('hide');
            swal.fire('Tebrikler!', 'Kullanıcı Eklendi.', 'success');
        },
        error: function () {
            $(".page-loader-wrapper").hide();
        }
    });
});

$("updateUserForm").on("submit", function (event) {
    event.preventDefault();
    $(".page-loader-wrapper").show();
    var data = convertFormToJSON($(this));
    var json = JSON.stringify(data);

    $.ajax({
        url: baseApiUrl + '/user',
        type: 'PUT',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: json,
        success: function (data) {
            $(".page-loader-wrapper").hide();
            $('#userTable').DataTable().ajax.reload();
            $('#updateUserFormModal').modal('hide');
            swal.fire('Tebrikler!', 'Kullanıcı Güncellendi.', 'success');
        },
        error: function () {
            $(".page-loader-wrapper").hide();
        }
    });
});
function convertFormToJSON(form) {
    const array = $(form).serializeArray();
    const json = {};
    $.each(array, function () {
        json[this.name] = this.value || "";
    });
    return json;
}