function getPassports() {
    var id = localStorage['role'].indexOf('User') > -1 ? localStorage['user_id'] : null;
    var data = { masterId: id };

    $.ajax({
        url: "http://localhost:46985/api/pet",
        dataType: "json",
        method: "GET",
        data: data,
        origin: "http://localhost:24744",
        success: function (e) {
            showTable(e);
        }
    });
}

function getPassport() {
    var data = { id: $('#Id').val() };

    $.ajax({
        url: "http://localhost:46985/api/pet",
        dataType: "json",
        data: data,
        method: "GET",
        origin: "http://localhost:24744",
        success: function (e) {
            var img = getImageName(e);
            $('#pet-pic').append('<img style="max-width: 100%;" src="' + img + '">');

            var list = $('<dl class="dl-horizontal">' +
                        '<dt>Кличка:</dt>' +
                        '<dd>' + e.Name + '</dd>' +
                        '<dt>Тип:</dt>' +
                        '<dd>' + e.Breed.Type.Name + '</dd>' +
                        '<dt>Порода:</dt>' +
                        '<dd>' + e.Breed.Name + '</dd>' +
                        '<dt>Дата рождения:</dt>' +
                        '<dd>' + e.BirthDate.split('T')[0] + '</dd>'+ 
                        '<dt>Пол:</dt>' +
                        '<dd>' + (e.Gender ? 'М' : 'Ж') + '</dd>' +
                        '<dt>Особые отметины:</dt>' +
                        '<dd>' + (e.Special != null ? e.Special : '') + '</dd>' +
                     '</dl>');
            $('#pet-info').append('<h4>Информация о питомце:</h4>');
            $('#pet-info').append(list);

            var select = $('<select multiple="multiple" style="width: 100%">');
            for (var i = 0; i < e.VaccinationsForPet.length; i++) {
                $(select).append('<option value="' + e.VaccinationsForPet[i].Vaccination.Id + '">' + e.VaccinationsForPet[i].Vaccination.Name + '</option>');
            }
            $('#pet-vac').append('<h4>Вакцины:</h4>');
            $('#pet-vac').append(select);

            var listOwn = $('<dl class="dl-horizontal">' +
                        '<dt>ФИО:</dt>' +
                        '<dd>' + e.Master.Surname + ' ' + e.Master.Name + ' ' + e.Master.Patronymic + '</dd>' +
                        '<dt>Адрес:</dt>' +
                        '<dd>' + (e.Master.Address != null ? e.Master.Address : '') + '</dd>' +
                        '<dt>Контакты:</dt>' +
                        '<dd>' + (e.Master.Contacts != null ? e.Master.Contacts : '') + '</dd>' +
                     '</dl>');
            $('#pet-own').append('<h4>Информация о владельце:</h4>');
            $('#pet-own').append(listOwn);
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

function showTable(data) {
    for (var i = 0; i < data.length; i++) {
        var tr = $('<tr>');
        var master = data[i].Master;

        tr.append($('<td>' + master.Surname + ' ' + master.Name + ' ' + master.Patronymic + '</td>'));
        tr.append($('<td>' + data[i].Name + '</td>'));
        tr.append($('<td>' + data[i].Breed.Type.Name + '</td>'));
        tr.append($('<td>' + data[i].Breed.Name + '</td>'));
        tr.append($('<td>' + data[i].BirthDate.split('T')[0] + '</td>'));
        tr.append($('<td>' +
                        '<a href="/Passport/Index?id=' + data[i].Id + '">Подробнее</a>' +
                    '</td>'
        ));

        $('tbody').append(tr);
    }
}