var tabelaUsuarios;

$(document).ready(function () {
    tabelaUsuarios = $('#tabelaUsuario').DataTable({
        'createdRow': function (row, data, dataIndex) {
            $(row).addClass('usuario' + data[0]);
        },
        dom: 'Bfrtip'
    });

    carregaTabela();
});

function excluirUsuario(id) {
    if (confirm('Deseja realmenta apagar esse usuario?\nEssa ação não poderá ser desfeita.')) {
        $.ajax({
            url: '/Usuario/ExcluirConfirmacao',
            method: 'post',
            data: { id: parseInt(id) },
            dataType: 'json',
            success(data) {
                if (data == "sucesso")
                    //location.reload(true);
                    tabelaUsuarios.rows('.usuario' + id).remove().draw(); //removendo a linha
            },
            error(e) {
                alert('Não foi possivel concluir a operação!\nEntre em contato com o administrador do sistema.')
                console.log(e);
            }
        })
    }
}

function carregaTabela() {
    tabelaUsuarios.clear().draw();

    $.ajax({
        url: '/Usuario/ListaUsuarios',
        method: 'get',
        dataType: 'json',
        success(data) {
            if (data != null) {
                montaTabela(data);
            }
        },
        error(e) {
            console.error('ERRO');
            console.log(e);
            alert('Desculpe, estamos com um problema de conexão!\nPor favor, entre em contato com o administrador do sistema!');
        }
    })
}

function montaTabela(usuarios) {
    tabelaUsuarios.clear().draw();

    let rows = [];

    $.each(usuarios, function (i, usuario) {
        //Id
        //Nome
        //Email
        //CPF
        //Celular
        //Adm (boolean)
        //Dev (boolean)

        let botoes = '<a href="/Usuario/Atualizar?id=' + usuario.UsuarioID + '" class="btn btn-warning btn-xs" title="Atualizar usuário"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;&nbsp;' +
            '<a href="#" onclick="excluirUsuario(' + usuario.UsuarioID + ')" class="btn btn-danger btn-xs" title="Apagar usuário"><span class="glyphicon glyphicon-remove"></span></a>';

        let row = [
            usuario.UsuarioID,
            usuario.Nome,
            usuario.Email,
            usuario.CPF,
            usuario.Celular,
            usuario.Adm ? 'Sim' : 'Não',
            usuario.Dev ? 'Sim' : 'Não',
            botoes
        ]

        rows.push(row);
    })

    tabelaUsuarios.rows.add(rows).draw();
}