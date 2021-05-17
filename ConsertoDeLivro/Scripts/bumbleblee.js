var usuarioEncontrado = false;
var email;
var cpf;
var senha;
var larg, alt, exp;

function trocarSenha() {
    email = $('input[name="Email"]').val();
    cpf = $('input[name="CPF"]').val();
    usuarioEncontrado = false;
    if (email != null || cpf != null) {
        $.ajax({
            url: '/Usuario/ListaUsuarios',
            method: 'get',
            dataType: 'json',
            success(data) {
                if (data != null) {
                    $.each(data, function (name, value) {
                        if (value.Email == email && value.CPF == cpf) {
                            //Inputs
                            $('div[id="divCpfEmail"]').css('display', 'none');
                            $('div[id="divSenha"]').css('display', 'flex');

                            //Botoes
                            $('a[id="continuar"]').css('display', 'none');
                            $('a[id="updtSenha"]').css('display', 'block');

                            usuarioEncontrado = true;
                        }
                    });

                    if (!usuarioEncontrado) {
                        alert('Usuario não encontrado!\nOs dados não pertencem a nenhum usuário cadastrado no sistema!');
                    }
                }
            },
            error(e) {
                console.log(e);
                bootbox.alert('Deu um problema.\nEntre em contato com o Administrador do sistema!');
            }
        });
    } else {
        bootbox.alert('Os dois campos precisam ser preenchidos.');
    }
}

function confirmarSenha() {
    var priSenha = $('input[name="priSenha"]').val();
    var secSenha = $('input[name="secSenha"]').val();
    if ((priSenha != null && secSenha != null) &&
        (priSenha.length >= 6 && priSenha.length <= 13)) {
        if (priSenha == secSenha) {
            senha = $('input[name="priSenha"]').val();
            $.ajax({
                url: '/ConsertoDeLivro/TrocarSenha',
                method: 'post',
                data: { cpf: cpf, email: email, senha: senha },
                success(data) {
                    console.log('dados enviados');
                    if (alert('Sua senha foi alterada com sucesso!')) {
                        //nao é para vir aqui
                        location.href = '/ConsertoDeLivro/Index';
                    } else {
                        window.location.href = '/ConsertoDeLivro/Login';
                    }
                },
                error(e) {
                    alert('Erro na conexão!\nEntre em contato com o administrador do sistema.');
                }
            });
        } else {
            alert('As senha não correspondem!')
            $('label[class="msgErro"]').css('display', 'flex');
        }
    } else {
        alert('Atenção!\nPreencha os dados corretamente!')
    }
}

function campoNumerico(key) {
    //console.log(key.keyCode);

    if ($('#ValorMetro').val().length >= 6) {
        return false;
    }

    if ((key.keyCode < 48 || key.keyCode > 57) && //numeros
        (key.keyCode < 37 || key.keyCode > 40) &&
        key.keyCode != 8) {
        key.preventDefault();
        return false;
    }

    if ((key.keyCode >= 48 && key.keyCode <= 57) || key.keyCode == 8)
        conferirCampoValor(key);
}

function conferirCampoValor(key) {
    var campoValor = $('#ValorMetro').val();
    console.log('tamanho: ' + campoValor.length);
    if (key.keyCode != 8) {
        if (campoValor.length == 2) {
            $('#ValorMetro').val(campoValor.slice(0, 1) + ',' + campoValor.slice(1));
        }

        if (campoValor.length == 4) {
            $('#ValorMetro').val(campoValor.replace(',', '').slice(0, 2) + ',' + campoValor.slice(3));
        }

        if (campoValor.length == 5) {
            $('#ValorMetro').val(campoValor.replace(',', '').slice(0, 3) + ',' + campoValor.slice(4));
        }
    }
}

function teste(tag, key) {

    let cod = key.keyCode || key.which;

    if (cod < 48 || cod > 57 || tag.value.length > 2) {
        key.preventDefault();
    }
}

function hihi(tag, key) {
    larg = tag.id == 'Largura' ? tag.value : larg;
    alt = tag.id == 'Comprimento' ? tag.value : alt;
    exp = tag.id == 'Expessura' ? tag.value : exp;

    console.log(larg + ' | ' + alt + ' | ' + exp);

    larg = parseInt(larg);
    alt = parseInt(alt);
    exp = parseInt(exp);

    let valor = 40;

    if (larg > 0 && alt > 0 && (exp != undefined && !isNaN(exp))) {
        let preco = (((larg + exp + 5) / 100) * (valor / 2)) + (((alt + 5) / 100) * (valor / 2));
        let preco_final = parseFloat(preco + 15).toFixed(2).replace('.',',');
        $('#preco').text(preco_final);
    }
}