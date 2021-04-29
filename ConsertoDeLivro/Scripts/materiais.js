var tabelaMateriais;

$(document).ready(function () {
    tabelaMateriais = $('#listaMateriais').DataTable({
        pagingType: 'simple_numbers',
        language: {
            emptyTable: 'Nenhum registro encontrado',
            info: 'Mostrando _START_ até _END_ de _TOTAL_ registros',
            infoEmpty: '',
            search: 'Pesquisar',
        },
        order: [],
        dom: 'Bfrtip'
    });

    carregaMateriais();
});

function carregaMateriais() {
    console.log('Carrega Materiais');

    tabelaMateriais.clear().draw();

    $.ajax({
        url: '/Material/Materiais',
        method: 'get',
        dataType: 'json',
        success(data) {
            console.log('dados recebidos');
            if (data != null) {
                montaTabela(data);
            }
        },
        error(e) {
            console.log('erro');
            console.log(e);
            alert('Erro na comunicação com o servidor.\nPor favor, entre em contato com o administrador do sistema!');
        }
    })
}

function montaTabela(materiais) {
    tabelaMateriais.clear().draw();

    let rows = [];

    $.each(materiais, function (i, material) {
        //Id
        //Nome
        //Preço
        //Descrição
        //Opções

        let botoes = '<a href="/Material/AtualizarMaterial?id=' + material.Id + '" class="btn btn-warning btn-xs" title="Editar registro"><span class="glyphicon glyphicon-pencil"></a>&nbsp;&nbsp;' +
            '<a href="/Material/ApagarMaterial?id=' + material.Id + '" class="btn btn-danger btn-xs" title="Apagar registro"><span class="glyphicon glyphicon-remove"></span></a>';

        let row = [
            material.Id,
            material.Nome,
            material.ValorMetro,
            material.Descricao,
            botoes
        ]

        rows.push(row);
    })

    tabelaMateriais.rows.add(rows).draw();
}