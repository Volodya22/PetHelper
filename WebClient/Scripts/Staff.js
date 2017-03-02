function staffDataIsValid() {
    var isOk = true;
    var names = ['Email', 
        'Surname', 'Name',
        'Patronymic', 'Specialization',
        'Education', 'Trophies'];
    var data = [
        $('#email-error'),
        $('#sname-error'),
        $('#name-error'),
        $('#patr-error'),
        $('#educ-error'),
        $('#spec-error'),
        $('#trop-error')
    ];
    var lengths = [30, 25, 25, 25, 50, 100, 150];

    for (var i = 0; i < names.length; i++) {
        if ($('#' + names[i]).val().length > lengths[i]) {
            $(data[i]).removeClass('hidden');
            isOk = false;
        } else {
            $(data[i]).addClass('hidden');
        }
    }

    if ($('#Password').val().length == 0 ||
        $('#Email').val().length == 0 ||
        $('#Surname').val().length == 0 ||
        $('#Name').val().length == 0) {
        $('#null-error').removeClass('hidden');
        isOk = false;
    } else {
        $('#null-error').addClass('hidden');
    }

    if ($('#Password').val().length < 5) {
        $('#pwd-error').removeClass('hidden');
        isOk = false;
    } else {
        $('#pwd-error').addClass('hidden');
    }

    if (isOk) {
        $("#errors").addClass('hidden');
    } else {
        $("#errors").removeClass('hidden');
    }

    return isOk;
}

function getStaff(roleId, forTable) {
    var role = { roleId: roleId };

    $.ajax({
        url: "http://localhost:46985/api/user",
        dataType: "json",
        method: "GET",
        data: role,
        origin: "http://localhost:24744",
        success: function (data) {
            if (roleId == 2) {
                if (forTable) {
                    showTable(data);
                } else {
                    showData(data);
                }
            } else {
                showSelect(data);
            }
        }
    });
}

function getEmp(toEdit) {
    var data = { id: $('#Id').val() };

    $.ajax({
        url: "http://localhost:46985/api/user",
        dataType: "json",
        data: data,
        method: "GET",
        origin: "http://localhost:24744",
        success: function (e) {
            if (toEdit) {
                fillEdit(e);
            } else {
                fillDelete(e);
            }
        }
    });
}

function postEmp() {
    var data = {
        Email: $('#Email').val(),
        Password: $('#Password').val(),
        Surname: $('#Surname').val(),
        Name: $('#Name').val(),
        Patronymic: $('#Patronymic').val(),
        Gender: $('#Gender').val(),
        BirthDate: $('#BirthDate').val(),
        Specialization: $('#Specialization').val(),
        Education: $('#Education').val(),
        Trophies: $('#Trophies').val(),
        Roles: [{
            Id: 2,
            Name: "Doctor"
        }]
    };

    $.ajax({
        url: "http://localhost:46985/api/user",
        dataType: "json",
        data: data,
        method: "POST",
        origin: "http://localhost:24744",
        success: function (e) {
            $('#Id').val(e);
            alert('Пользователь был успешно создан!');
            $('form').submit();
        }
    });
}

function putEmp() {
    var data = {
        Id: $('#Id').val(),
        Email: $('#Email').val(),
        Password: $('#Password').val(),
        Surname: $('#Surname').val(),
        Name: $('#Name').val(),
        Patronymic: $('#Patronymic').val(),
        Gender: $('#Gender').val(),
        BirthDate: $('#BirthDate').val(),
        ImageUrl: $('#ImageUrl').val(),
        Specialization: $('#Specialization').val(),
        Education: $('#Education').val(),
        Trophies: $('#Trophies').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/user/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "PUT",
        origin: "http://localhost:24744",
        success: function () {
            alert('Пользователь был успешно отредактирован!');
        }
    });
}

function deleteEmp() {
    var data = {
        "id": $('#Id').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/user/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "DELETE",
        origin: "http://localhost:24744",
        success: function () {
            alert('Пользователь был успешно удален!');
        }
    });
}

function showSelect(data) {
    var select = $('#MasterId');
    for (var i = 0; i < data.length; i++) {
        $(select).append('<option value="' + data[i].Id + '">' + data[i].Surname + ' ' + data[i].Name + '</option>');
    }
}

function showTable(data) {
    for (var i = 0; i < data.length; i++) {
        var tr = $('<tr>');
        var patr = data[i].Patronymic != null ? data[i].Patronymic : "-";

        tr.append($('<td>' + data[i].Surname + '</td>'));
        tr.append($('<td>' + data[i].Name + '</td>'));
        tr.append($('<td>' + patr + '</td>'));
        tr.append($('<td>' + data[i].BirthDate.split('T')[0] + '</td>'));
        tr.append($('<td>' +
                        '<a href="/Staff/Edit?id=' + data[i].Id + '">Редактировать</a> | ' +
                        '<a href="/Staff/Delete?id=' + data[i].Id + '">Удалить</a>' +
                    '</td>'
        ));

        $('tbody').append(tr);
    }
}

function showData(data) {
    for (var i = 0; i < data.length; i++) {
        var wrap = $('<div class="col-md-12 doc">');
        var infoWrap = $('<div class="col-md-9">');
        var imgWrap = $('<div class="col-md-3 text-center">');
        var divText = $('<div class="text-center">');
        var spec = data[i].Specialization != null ? data[i].Specialization : '-';
        var educ = data[i].Education != null ? data[i].Education : "-";
        var trop = data[i].Trophies != null ? data[i].Trophies : '-';
        var list = $('<dl class="dl-horizontal doc-view">' +
                        '<dt>Специализация:</dt>' +
                        '<dd>' + spec + '</dd>' +
                        '<dt>Образование:</dt>' +
                        '<dd>' + educ + '</dd>' +
                        '<dt>Список наград и дипломов:</dt>' +
                        '<dd>' + trop + '</dd>' +
                     '</dl>');

        var patr = data[i].Patronymic != null ? data[i].Patronymic : "";
        divText.append($('<label>' + data[i].Surname + ' ' + data[i].Name + ' ' + patr + '</label>'));

        var img = getImage(data[i]);
        imgWrap.append($('<img style="max-height: 250px;" src="' + img + '">'));

        infoWrap.append(divText);
        infoWrap.append(list);

        wrap.append(infoWrap);
        wrap.append(imgWrap);
        wrap.append('<div style="clear: both;">');

        $('#content').append(wrap);
    }
}

function getImage(e) {
    var local = "http://localhost:24744/Content/images/";
    var ext = "http://localhost:46985/Content/images/";
    var img = e.ImageUrl;

    if (img == '' || img == null) {
        if (e.Gender) {
            img = ext + 'male-doc.jpg';
        } else {
            img = ext + 'female-doc.jpg';
        }
    } else {
        img = local + img;
    }

    return img;
}

function fillEdit(data) {
    $('#Email').val(data.Email);
    $('#Password').val(data.Password);
    $('#Surname').val(data.Surname);
    $('#Name').val(data.Name);
    $('#Patronymic').val(data.Patronymic);
    $('#BirthDate').val(data.BirthDate.split('T')[0]);
    $('#Gender').prop('checked', data.Gender);
    $('#Specialization').val(data.Specialization);
    $('#Education').val(data.Education);
    $('#Trophies').val(data.Trophies);
}

function fillDelete(data) {
    $($('dd')[0]).html(data.Surname);
    $($('dd')[1]).html(data.Name);
    $($('dd')[2]).html(data.Patronymic);
}