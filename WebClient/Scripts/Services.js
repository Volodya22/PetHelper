function serviceIsValid() {
    var isOk = true;

    if ($('#Description').val().length == 0 ||
        $('#Name').val().length == 0 ||
        $('#Price').val().length == 0) {
        $("#null-error").removeClass('hidden');
        isOk = false;
    } else {
        $("#null-error").addClass('hidden');
    }

    if ($('#Name').val().length > 50) {
        $("#name-error").removeClass('hidden');
        isOk = false;
    } else {
        $("#name-error").addClass('hidden');
    }

    if ($('#Description').val().length > 150) {
        $("#desc-error").removeClass('hidden');
        isOk = false;
    } else {
        $("#desc-error").addClass('hidden');
    }

    if (isOk) {
        $("#errors").addClass('hidden');
    } else {
        $("#errors").removeClass('hidden');
    }

    return isOk;
}

function getServices(t) {
    $.ajax({
        url: "http://localhost:46985/api/service",
        dataType: "json",
        method: "GET",
        origin: "http://localhost:24744",
        success: function (data) {
            if (t != null) {
                for (var i = 0; i < data.length; i++) {
                    $('#ServiceId').append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
                }
            } else {
                for (var i = 0; i < data.length; i++) {
                    var tr = $('<tr>');

                    tr.append($('<td>' + data[i].Name + '</td>'));
                    tr.append($('<td>' + data[i].Price + '</td>'));
                    tr.append($('<td>' + data[i].Description + '</td>'));
                    tr.append($('<td class="options hidden">' +
                                    '<a href="/Service/Edit?id=' + data[i].Id + '">Редактировать</a> | ' +
                                    '<a href="/Service/Delete?id=' + data[i].Id + '">Удалить</a>' +
                                '</td>'
                    ));

                    $('tbody').append(tr);

                    checkVisibility();
                }
            }
        }
    });
}

function checkVisibility() {
    if (localStorage['role'].indexOf('Admin') > -1) {
        $('p').removeClass('hidden');
        $('.options').removeClass('hidden');
    }
}

function getService() {
    var e = { id: $('#Id').val() };

    $.ajax({
        url: "http://localhost:46985/api/service",
        dataType: "json",
        data: e,
        method: "GET",
        origin: "http://localhost:24744",
        success: function (data) {
            if ($('#Name')[0] != null) {
                $('#Name').val(data.Name);
                $('#Price').val(data.Price);
                $('#Description').val(data.Description);
            } else {
                $($('dd')[0]).html(data.Name);
                $($('dd')[1]).html(data.Price);
                $($('dd')[2]).html(data.Description);
            }
        }
    });
}

function postService() {
    var data = {
        Name: $('#Name').val(),
        Price: $('#Price').val(),
        Description: $('#Description').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/service",
        dataType: "json",
        data: data,
        method: "POST",
        origin: "http://localhost:24744",
        success: function () {
            //alert('Услуга была успешно создана!');
        }
    });
}

function putService() {
    var data = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        Price: $('#Price').val(),
        Description: $('#Description').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/service/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "PUT",
        origin: "http://localhost:24744",
        success: function () {
            //alert('Услуга была успешно отредактирована!');
        }
    });
}

function deleteService() {
    var data = {
        "id": $('#Id').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/service/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "DELETE",
        origin: "http://localhost:24744",
        success: function () {
            //alert('Услуга был успешно удалена!');
        }
    });
}

function getServicesForPets() {
    var id = localStorage['role'].indexOf('User') > -1 ? localStorage['user_id'] : null;
    var e = { masterId: id };

    $.ajax({
        url: "http://localhost:46985/api/serviceforpet",
        dataType: "json",
        method: "GET",
        data: e,
        origin: "http://localhost:24744",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var tr = $('<tr>');

                tr.append($('<td>' + data[i].Pet.Name + '</td>'));
                tr.append($('<td>' + data[i].Service.Name + '</td>'));
                tr.append($('<td>' + data[i].Date.split('T')[0] + '</td>'));
                tr.append($('<td>' +
                                '<a href="/Visit/Edit?id=' + data[i].Id + '">Редактировать</a> | ' +
                                '<a href="/Visit/Delete?id=' + data[i].Id + '">Удалить</a>' +
                            '</td>'
                ));

                $('tbody').append(tr);
            }
        }
    });
}

function getServiceForPet() {
    var e = { id: $('#Id').val() };

    $.ajax({
        url: "http://localhost:46985/api/serviceforpet",
        dataType: "json",
        data: e,
        method: "GET",
        origin: "http://localhost:24744",
        success: function (data) {
            if ($('#Date')[0] != null) {
                $('#PetId').val(data.PetId);
                $('#ServiceId').val(data.ServiceId);
                $('#Date').val(data.Date.split('T')[0]);
            } else {
                $($('dd')[0]).html(data.Pet.Name);
                $($('dd')[1]).html(data.Service.Name);
                $($('dd')[2]).html(data.Date.split('T')[0]);
            }
        }
    });
}

function postServiceForPet() {
    var data = {
        PetId: $('#PetId').val(),
        ServiceId: $('#ServiceId').val(),
        Date: $('#Date').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/serviceforpet",
        dataType: "json",
        data: data,
        method: "POST",
        origin: "http://localhost:24744",
        success: function () {
            //alert('Визит был успешно создан!');
        }
    });
}

function putServiceForPet() {
    var data = {
        Id: $('#Id').val(),
        PetId: $('#PetId').val(),
        ServiceId: $('#ServiceId').val(),
        Date: $('#Date').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/serviceforpet/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "PUT",
        origin: "http://localhost:24744",
        success: function () {
            //alert('Визит был успешно отредактирован!');
        }
    });
}

function deleteServiceForPet() {
    var data = {
        "id": $('#Id').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/serviceforpet/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "DELETE",
        origin: "http://localhost:24744",
        success: function () {
            //alert('Визит был успешно удален!');
        }
    });
}