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
            { data: 'password' }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
};