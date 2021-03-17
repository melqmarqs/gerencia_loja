var usuarioEncontrado = false;
var email;
var cpf;
var senha;

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
                            $('div[id="divSenha"]').css('display', 'block');

                            //Botoes
                            $('a[id="continuar"]').css('display', 'none');
                            $('a[id="updtSenha"]').css('display', 'block');

                            usuarioEncontrado = true;
                        }
                    });

                    if (!usuarioEncontrado) {
                        bootbox.alert({
                            message: 'Usuario não encontrado! <br>Os dados não pertencem a nenhum usuário cadastrado no sistema!',
                            backdrop: true,
                        });
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

function confirmar() {
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
                },
                error(e) {
                    alert('Erro na conexão');
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