var map;

jQuery(function ($) {
    if ($('input[name="CEP"]')) {
        $('input[name="CEP"]').change(function () {
            var cep = $(this).val();
            if (cep.length == 0) {
                LimparTextBoxesEndereco();
                return;
            }
            $.get("https://ws.apicep.com/cep.json", { code: cep }, function (result) {
                if (result.status != 200) {
                    $(this).focus();
                    alert(result.message || 'O CEP não foi encontrado.');
                    LimparTextBoxesEndereco();
                    return;
                }

                //mudando o foco para a textbox de numero
                $('input[name="Numero"]').focus();
                //preenchendo os campos com os dados da consulta
                $('input[name="Logradouro"]').val(result.address);
                $('input[name="Bairro"]').val(result.district);
                $('input[name="Cidade"]').val(result.city);
                $('select[name="EstadoID"]>option:eq(' + UFEstado(result.state) + ')').attr('selected', true);
            });
        });
    }
});

function LimparTextBoxesEndereco() {
    $('input[name="Logradouro"]').val("");
    $('input[name="Bairro"]').val("");
    $('input[name="Cidade"]').val("");
    $('input[name="Complemento"]').val("");
    $('input[name="Numero"]').val("");
    $('select[name="EstadoID"] option[selected="selected"]').removeAttr("selected");
}

function UFEstado(uf) {
    switch (uf) {
        case 'AC':
            return 0;
            break;
        case 'AL':
            return 1;
            break;
        case 'AP':
            return 2;
            break;
        case 'AM':
            return 3;
            break;
        case 'BA':
            return 4;
            break;
        case 'CE':
            return 5;
            break;
        case 'DF':
            return 6;
            break;
        case 'ES':
            return 7;
            break;
        case 'GO':
            return 8;
            break;
        case 'MA':
            return 9;
            break;
        case 'MT':
            return 10;
            break;
        case 'MS':
            return 11;
            break;
        case 'MG':
            return 12;
            break;
        case 'PA':
            return 13;
            break;
        case 'PB':
            return 14;
            break;
        case 'PR':
            return 15;
            break;
        case 'PE':
            return 16;
            break;
        case 'PI':
            return 17;
            break;
        case 'RJ':
            return 18;
            break;
        case 'RN':
            return 19;
            break;
        case 'RS':
            return 20;
            break;
        case 'RO':
            return 21;
            break;
        case 'RR':
            return 22;
            break;
        case 'SC':
            return 23;
            break;
        case 'SP':
            return 24;
            break;
        case 'SE':
            return 25;
            break;
        case 'TO':
            return 26;
            break;
        default:
            return;
    }
}

function initMap() {
    //const latlong = new google.maps.LatLng(-22.718753116860718, -47.17262446897254);
    const latlong = new google.maps.LatLng(-22.72391290343944, -47.17970839312079);

    if (navigator.geolocation) {
        console.log(navigator.geolocation.getCurrentPosition(posicaoAtual));
    }

    map = new google.maps.Map(document.getElementById('map'), {
        center: latlong,
        zoom: 14
    });

    let icon = {
        url: '/Images/location-pin_blue.png',
        scaledSize: new google.maps.Size(30, 30)
    }

    const mapMarker = new google.maps.Marker({
        position: latlong,
        map: map,
        title: 'Conserto e Personalização de Livros',
        icon: icon
    });

    const infoUserPos = new google.maps.InfoWindow({
        content: 'Nós estamos aqui!',
        //position: latlong,
        //map: map
    });

    infoUserPos.open(map, mapMarker);

}

function posicaoAtual(position) {
    console.log(position);

    const pos = { lat: position.coords.latitude, lng: position.coords.longitude };

    let icon = {
        url: '/Images/location-pin_red.png',
        scaledSize: new google.maps.Size(30, 30)
    }

    let currentPosition = new google.maps.Marker({
        position: pos,
        map: map,
        title: 'Sua posição',
        icon: icon
    });

    const infoUserPos = new google.maps.InfoWindow({
        content: 'Você está aqui!',
        //position: pos,
        //map: map
    });

    infoUserPos.open(map, currentPosition);

    map.setCenter(pos);

}