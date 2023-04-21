function getUsers() {
    debugger;
    var table = $('#userTable');
    table.DataTable({
        ajax: {
            url: baseApiUrl + '/User',
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
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'userName' },
            { data: 'eMail' },
            { data: 'password' },
            {
                data: 'Process',
                "render": function (data, type, full, meta) {
                    return `<a class="btn btn-sm btn-custom-warning" data-toggle="modal" data-target="#editStudentModal" onclick="editStudentModal(`+ full['id'] + `);"><i class="la la-pencil"></i></a>
                            <a href="javascript:void(0);" class="btn btn-sm btn-custom-danger sweet-success-cancel" onclick="DeleteUser(`+ full['id'] + `,'studentTable');"><i class="la la-trash-o"></i></a>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
};