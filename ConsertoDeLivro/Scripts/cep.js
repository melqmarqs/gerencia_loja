jQuery(function ($) {
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

            //mudando o focus para a textbox de numero
            $('input[name="Numero"]').focus();
            //preenchendo os campos com os dados da consulta
            $('input[name="Logradouro"]').val(result.address);
            $('input[name="Bairro"]').val(result.district);
            $('input[name="Cidade"]').val(result.city);
            $('select[name="EstadoID"]>option:eq(' + UFEstado(result.state) + ')').attr('selected', true);
        });
    });
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
            return 1;
            break;
        case 'AL':
            return 2;
            break;
        case 'AP':
            return 3;
            break;
        case 'AM':
            return 4;
            break;
        case 'BA':
            return 5;
            break;
        case 'CE':
            return 6;
            break;
        case 'DF':
            return 7;
            break;
        case 'ES':
            return 8;
            break;
        case 'GO':
            return 9;
            break;
        case 'MA':
            return 10;
            break;
        case 'MT':
            return 11;
            break;
        case 'MS':
            return 12;
            break;
        case 'MG':
            return 13;
            break;
        case 'PA':
            return 14;
            break;
        case 'PB':
            return 15;
            break;
        case 'PR':
            return 16;
            break;
        case 'PE':
            return 17;
            break;
        case 'PI':
            return 18;
            break;
        case 'RJ':
            return 19;
            break;
        case 'RN':
            return 20;
            break;
        case 'RS':
            return 21;
            break;
        case 'RO':
            return 22;
            break;
        case 'RR':
            return 23;
            break;
        case 'SC':
            return 24;
            break;
        case 'SP':
            return 25;
            break;
        case 'SE':
            return 26;
            break;
        case 'TO':
            return 27;
            break;
        default:
            return ;
    }
}