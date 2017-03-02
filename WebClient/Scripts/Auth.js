function regDataIsValid(flag) {
    var isOk = true;

    if ($('#Email').val().length == 0 ||
        $('#Password').val().length == 0) {
        $("#null-error").removeClass('hidden');
        isOk = false;
    } else {
        $("#null-error").addClass('hidden');
    }

    if ($('#Email').val().length > 30) {
        $("#email-error").removeClass('hidden');
        isOk = false;
    } else {
        $("#email-error").addClass('hidden');
    }

    if ($('#Password').val().length < 5) {
        $("#pwd-error").removeClass('hidden');
        isOk = false;
    } else {
        $("#pwd-error").addClass('hidden');
    }

    if (flag == 1) {
        if ($('#Name').val().length == 0 ||
            $('#Surname').val().length == 0) {
            $("#null-error").removeClass('hidden');
            isOk = false;
        } else {
            $("#null-error").addClass('hidden');
        }

        if ($('#Surname').val().length > 25) {
            $("#sname-error").removeClass('hidden');
            isOk = false;
        } else {
            $("#sname-error").addClass('hidden');
        }

        if ($('#Name').val().length > 25) {
            $("#name-error").removeClass('hidden');
            isOk = false;
        } else {
            $("#name-error").addClass('hidden');
        }
    }

    if (isOk) {
        $("#errors").addClass('hidden');
    } else {
        $("#errors").removeClass('hidden');
    }

    return isOk;
}

function postAuth() {
    var data = {
        Email: $('#Email').val(),
        Password: $('#Password').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/account",
        dataType: "json",
        data: data,
        method: "POST",
        async: false,
        origin: "http://localhost:24744",
        success: function (e) {
            localStorage['user'] = e.split(';')[0];
            localStorage['user_id'] = e.split(';')[1];
            localStorage['role'] = e.split(';')[2];
        }
    });
}

function postRegister() {
    var data = {
        Email: $('#Email').val(),
        Password: $('#Password').val(),
        Surname: $('#Surname').val(),
        Name: $('#Name').val(),
        Patronymic: $('#Patronymic').val(),
        Gender: $('#Gender').val(),
        BirthDate: $('#BirthDate').val(),
        Address: $('#Address').val(),
        Contacts: $('#Contacts').val(),
        Roles: [{
            Id: 3,
            Name: "User"
        }]
    };

    $.ajax({
        url: "http://localhost:46985/api/user",
        dataType: "json",
        data: data,
        method: "POST",
        async: false,
        origin: "http://localhost:24744",
        success: function () {
            setTimeout(function() {
                postAuth();
            });
        }
    });
}