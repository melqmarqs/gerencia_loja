var usuarioEncontrado = false;

function trocarSenha() {
    var email = $('input[name="Email"]').val();
    var cpf = $('input[name="CPF"]').val();
    usuarioEncontrado = false;
    $.ajax({
        url: '/Usuario/ListaUsuarios',
        method: 'get',
        dataType: 'json',
        success(data) {
            if (data != null) {
                $.each(data, function (name, value) {
                    if (value.Email == email && value.CPF == cpf) {
                        bootbox.alert('Dados válidos.\nPreencha atenciosamente com a sua nova senha.');

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
            bootbox.alert("Deu um problema.\nEntre em contato com o Administrador do sistema!");
        }
    });
}