var petTypes = null;

function petIsValid() {
    var isOk = true;

    if ($('#Name').val().length == 0) {
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

    if (isOk) {
        $("#errors").addClass('hidden');
    } else {
        $("#errors").removeClass('hidden');
    }

    return isOk;
}

function getCards(t) {
    var id = localStorage['role'].indexOf('User') > -1 ? localStorage['user_id'] : null;
    var data = { masterId: id };

    $.ajax({
        url: "http://localhost:46985/api/pet",
        dataType: "json",
        method: "GET",
        data: data,
        origin: "http://localhost:24744",
        success: function (e) {
            if (t != null) {
                showDropDown(e);
            } else {
                showTable(e);
            }
        }
    });
}

function showDropDown(e) {
    for (var i = 0; i < e.length; i++) {
        $('#PetId').append('<option value="' + e[i].Id + '">' + e[i].Name + '</option>');
    }
}

function getCard(reason) {
    var data = { id: $('#Id').val() };

    $.ajax({
        url: "http://localhost:46985/api/pet",
        dataType: "json",
        data: data,
        method: "GET",
        origin: "http://localhost:24744",
        success: function (e) {
            if (reason == 0) {
                showData(e);
            } else if (reason == 1) {
                fillCardEdit(e);
            } else {
                fillDelete(e);
            }
        }
    });
}

function getVacs(type, flag) {
    var data = { typeId: type };

    $.ajax({
        url: "http://localhost:46985/api/vaccination",
        dataType: "json",
        data: data,
        method: "GET",
        origin: "http://localhost:24744",
        success: function (e) {
            if (flag == null) {
                var select = $('<select multiple="multiple" style="width: 100%">');
                for (var i = 0; i < e.length; i++) {
                    $(select).append('<option value="' + e[i].Id + '">' + e[i].Name + '</option>');
                }
                $('#vacs').append('<h4>Необходимые вакцины:</h4>');
                $('#vacs').append(select);
            } else {
                $('#vacs-total').html("");
                for (var i = 0; i < e.length; i++) {
                    $('#vacs-total').append('<option value="' + e[i].Id + '">' + e[i].Name + '</option>');
                }
            }
        }
    });
}

function getImageName(e) {
    var local = 'http://localhost:24744/Content/images/';
    var ext = 'http://localhost:46985/Content/images/';

    if (e.ImageUrl != null && e.ImageUrl != "") {
        return local + e.ImageUrl;
    }

    switch (e.Breed.Type.Id) {
        case 1:
            return ext + "dog.gif";
        case 2:
            return ext + "cat.png";
        case 3:
            return ext + "parrot.jpg";
    }
}

function postCard() {
    var data = {
        Name: $('#Name').val(),
        MasterId: $('#MasterId').val(),
        BreedId: $('#BreedId').val(),
        BirthDate: $('#BirthDate').val(),
        Gender: $('#Gender').val(),
        Special: $('#Special').val(),
        Weight: $('#Weight').val(),
        VaccinationsForPet: []
    };

    var options = $('#vacs option');
    for (var i = 0; i < options.length; i++) {
        data.VaccinationsForPet.push({ VaccinationId: $(options[i]).val() });
    }

    $.ajax({
        url: "http://localhost:46985/api/pet",
        dataType: "json",
        data: data,
        method: "POST",
        origin: "http://localhost:24744",
        success: function (e) {
            $('#Id').val(e);
            alert('Питомец был успешно создан!');
            $('form').submit();
        }
    });
}

function getType() {
    var data = { id: 4 };

    $.ajax({
        url: "http://localhost:46985/api/vaccination",
        dataType: "json",
        data: data,
        method: "GET",
        origin: "http://localhost:24744",
        success: function (e) {
            petTypes = e.PetTypes;

            fillSelect('TypeId', e.PetTypes);
            fillSelect('BreedId', e.PetTypes[0].PetBreeds);
        }
    });
}

function fillSelect(id, data) {
    var select = $('#' + id);
    $(select).html("");
    for (var i = 0; i < data.length; i++) {
        $(select).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
    }
}

function refreshBreed(id) {
    var data = petTypes[id - 1].PetBreeds;
    fillSelect('BreedId', data);

    refreshVacs(id);
}

function refreshVacs(type) {
    $('#vacs').html("");
    getVacs(type, 1);
}

function addVac() {
    var id = $('#vacs-total').val();
    var name = $('#vacs-total option[value="' + id + '"]')[0].innerHTML;
    var options = $('#vacs option');

    for (var i = 0; i < options.length; i++) {
        if ($(options[i]).val() == id) {
            return;
        }
    }

    $('#vacs').append('<option value="' + id + '">' + name + '</option>');
}

function deleteVac() {
    var id = $('#vacs-total').val();
    var options = $('#vacs option');

    for (var i = 0; i < options.length; i++) {
        if ($(options[i]).val() == id) {
            $(options[i]).remove();
        }
    }
}

function putCard() {
    var data = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        MasterId: $('#MasterId').val(),
        BreedId: $('#BreedId').val(),
        BirthDate: $('#BirthDate').val(),
        Gender: $('#Gender').val(),
        Special: $('#Special').val(),
        Weight: $('#Weight').val(),
        VaccinationsForPet: []
    };

    var options = $('#vacs option');
    for (var i = 0; i < options.length; i++) {
        data.VaccinationsForPet.push({ VaccinationId: $(options[i]).val() });
    }

    $.ajax({
        url: "http://localhost:46985/api/pet/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "PUT",
        origin: "http://localhost:24744",
        success: function () {
            alert('Питомец был успешно отредактирован!');
        }
    });
}

function deleteCard() {
    var data = {
        "id": $('#Id').val()
    };

    $.ajax({
        url: "http://localhost:46985/api/pet/" + $('#Id').val(),
        dataType: "json",
        data: data,
        method: "DELETE",
        origin: "http://localhost:24744",
        success: function () {
            alert('Питомец был успешно удален!');
        }
    });
}

function showTable(data) {
    for (var i = 0; i < data.length; i++) {
        var tr = $('<tr>');
        var master = data[i].Master;
        var isAdmin = localStorage['role'].indexOf('Admin') > -1;
        var className = isAdmin ? '' : 'hidden';

        tr.append($('<td>' + master.Surname + ' ' + master.Name + ' ' + master.Patronymic + '</td>'));
        tr.append($('<td>' + data[i].Name + '</td>'));
        tr.append($('<td>' + data[i].BirthDate.split('T')[0] + '</td>'));
        tr.append($('<td>' + data[i].Breed.Type.Name + '</td>'));
        tr.append($('<td>' + data[i].Breed.Name + '</td>'));
        tr.append($('<td>' +
                        '<a class="' + className + '" href="/Card/Edit?id=' + data[i].Id + '">Редактировать</a> | ' +
                        '<a href="/Card/Index?id=' + data[i].Id + '">Подробнее</a> | ' +
                        '<a class="' + className + '" href="/Card/Delete?id=' + data[i].Id + '">Удалить</a>' +
                    '</td>'
        ));

        $('tbody').append(tr);

        if (isAdmin) {
            $('#card-cr').removeClass('hidden');
        }
    }
}

function showData(e) {
    $('#pet-name').append(e.Name);

    var img = getImageName(e);
    $('#pet-pic').append('<img style="max-width: 100%;" src="' + img + '">');

    var list = $('<dl class="dl-horizontal">' +
                    '<dt>Тип:</dt>' +
                    '<dd>' + e.Breed.Type.Name + '</dd>' +
                    '<dt>Порода:</dt>' +
                    '<dd>' + e.Breed.Name + '</dd>' +
                    '<dt>Дата рождения:</dt>' +
                    '<dd>' + e.BirthDate.split('T')[0] + '</dd>' +
                    '<dt>Пол:</dt>' +
                    '<dd>' + (e.Gender ? 'М' : 'Ж') + '</dd>' +
                    '<dt>Вес:</dt>' +
                    '<dd>' + e.Weight + '</dd>' +
                 '</dl>');
    $('#pet-info').append('<h4>Общая информация:</h4>');
    $('#pet-info').append(list);

    var select = $('<select multiple="multiple" style="width: 100%">');
    for (var i = 0; i < e.VaccinationsForPet.length; i++) {
        $(select).append('<option value="' + e.VaccinationsForPet[i].Vaccination.Id + '">' + e.VaccinationsForPet[i].Vaccination.Name + '</option>');
    }
    $('#pet-vac').append('<h4>Сделанные вакцины:</h4>');
    $('#pet-vac').append(select);

    getVacs(e.Breed.Type.Id);
}

function fillCardEdit(data) {
    $('#Name').val(data.Name);
    $('#Weight').val(data.Weight);
    $('#Gender').prop('checked', data.Gender);
    $('#Name').val(data.Name);
    $('#Special').val(data.Special);
    $('#BirthDate').val(data.BirthDate.split('T')[0]);
    $('#MasterId').val(data.Master.Id);

    setTimeout(function() {
        $('#TypeId').val(data.Breed.Type.Id);
        $('#TypeId').trigger('change');
        $('#BreedId').val(data.Breed.Id);

        for (var i = 0; i < data.VaccinationsForPet.length; i++) {
            $('#vacs').append('<option value="' + data.VaccinationsForPet[i].VaccinationId + '">' + data.VaccinationsForPet[i].Vaccination.Name + '</option>');
        }
    }, 100);
}

function fillDelete(data) {
    $($('dd')[0]).html(data.Master.Surname + ' ' + data.Master.Name);
    $($('dd')[1]).html(data.Name);
}