var usuarioEncontrado = false;
var email;
var cpf;
var senha;
var larg;
var alt;
var exp;
const aceitar = 'Aceitar';
const finalizar = 'Finalizar';
const entregar = 'Entregar';

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
    let cod = key.keyCode || key.which;

    if ($('#ValorMetro').val().length >= 6) {
        return false;
    }

    if ((cod < 48 || cod > 57) && //numeros
        (cod < 37 || cod > 40) &&
        cod != 8) {
        key.preventDefault();
        return false;
    }

    if ((cod >= 48 && cod <= 57) || cod == 8)
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

function apenasNumeros(tag, key) {

    let cod = key.keyCode || key.which;

    if (cod < 48 || cod > 57 || tag.value.length > 2) {
        key.preventDefault();
    }
}

function precoMaterial(tag, key) {
    larg = tag.id == 'Largura' ? tag.value : larg;
    alt = tag.id == 'Altura' ? tag.value : alt;
    exp = tag.id == 'Expessura' ? tag.value : exp;

    larg = parseInt(larg) || parseFloat($('#Largura').val());
    alt = parseInt(alt) || parseFloat($('#Altura').val());
    exp = parseInt(exp) || parseFloat($('#Expessura').val());

    if (larg > 0 && alt > 0 && (exp != undefined && !isNaN(exp))) {
        let matId = $('select[name="MaterialID"]')[0].value;
        $.ajax({
            url: '/Material/getMaterialById',
            method: 'get',
            dataType: 'json',
            data: { id: matId },
            success(data) {
                if (data == 'Sem dados') {
                    alert('Esse material não foi encontrado no sistema!');
                } else {
                    valor = parseFloat(data.ValorMetro);
                    calculaValor(valor);
                }
            },
            erro(e) {
                console.log(e);
                alert('Erro! Por favor, consulte o administrador do sistema!');
            }
        })
    } else {
        $('#Valor').text('0,00');
        $('input[name="Valor"]').val('0,00');
    }
}

function calculaValor(preco) {
    preco = (((larg + exp + 5) / 100) * (valor / 2)) + (((alt + 5) / 100) * (valor / 2));
    let preco_final = parseFloat(preco + 15).toFixed(2).replace('.', ',');
    $('#Valor').text(preco_final);
    $('input[name="Valor"]').val(preco_final);
}

function exibirDiv(tag) {

    //console.log($('#' + tag.id).next().css('display'));
    //console.log(tag.id);

    let elem = $('#' + tag.id).next();
    if (elem.css('display') == 'block') {
        elem.fadeOut();

        elem.children().each(function (index, div) {
            if (div.getAttribute('pedido')) {
                div.remove();
            }
        });

        $('#num' + tag.id).text('');

    } else {
        elem.fadeIn(1200);

        $.ajax({
            url: '/Pedido/getPedidos' + tag.id,
            method: 'get',
            dataType: 'json',
            success(data) {
                data.forEach(function (dados) {
                    let clone = elem.children().clone();
                    clone[0].removeAttribute('hidden');
                    let novoClone = clone[0];

                    novoClone.setAttribute('pedido', dados.PedidoID);

                    novoClone.querySelector('#idPedido').textContent = dados.PedidoID;
                    novoClone.querySelector('#cliente').textContent = dados.Usuario.Nome + ' ' + dados.Usuario.UltimoNome;
                    let numCelular = formatarNumCelular(dados.Usuario.Celular);
                    novoClone.querySelector('#numCelular').textContent = numCelular;

                    novoClone.querySelector('#largura').textContent = dados.Largura;
                    novoClone.querySelector('#altura').textContent = dados.Altura;
                    novoClone.querySelector('#expessura').textContent = dados.Expessura;

                    novoClone.querySelector('#data').textContent = dados.DataPedido;
                    novoClone.querySelector('#material').textContent = dados.Material.Nome;
                    novoClone.querySelector('#descricao').textContent = dados.Descricao;

                    if (novoClone.querySelector('#btnAcc'))
                        novoClone.querySelector('#btnAcc').setAttribute('onclick', 'acaoPedido(' + dados.PedidoID + ', \'' + aceitar + '\', \'' + tag.id + '\')');

                    if (novoClone.querySelector('#btnFinalizar'))
                        novoClone.querySelector('#btnFinalizar').setAttribute('onclick', 'acaoPedido(' + dados.PedidoID + ', \'' + finalizar + '\', \'' + tag.id + '\')');

                    if (dados.Usuario.Adm) {
                        if (dados.Entregue) {
                            if (novoClone.querySelector('#infoEntregue') && novoClone.querySelector('#btnEntregar')) {
                                novoClone.querySelector('#btnEntregar').setAttribute('hidden', '');
                                novoClone.querySelector('#btnEntregar').removeAttribute('class');
                                novoClone.querySelector('#infoEntregue').removeAttribute('hidden');
                            }
                        } else {
                            if (novoClone.querySelector('#infoEntregue') && novoClone.querySelector('#btnEntregar')) {
                                novoClone.querySelector('#infoEntregue').setAttribute('hidden', '');
                                novoClone.querySelector('#btnEntregar').removeAttribute('hidden');
                                novoClone.querySelector('#btnEntregar').setAttribute('btnpedido', dados.PedidoID);
                                novoClone.querySelector('#btnEntregar').setAttribute('onclick', 'acaoPedido(' + dados.PedidoID + ', \'' + entregar + '\', \'' + tag.id + '\')');
                            }
                        }
                    } else {
                        if (dados.Entregue) {
                            if (novoClone.querySelector('#infoRetirar') && novoClone.querySelector('#infoRetirado')) {
                                novoClone.querySelector('#infoRetirar').setAttribute('hidden', '');
                                novoClone.querySelector('#infoRetirado').removeAttribute('hidden');
                            }
                        } else {
                            if (novoClone.querySelector('#infoRetirar') && novoClone.querySelector('#infoRetirado')) {
                                novoClone.querySelector('#infoRetirar').removeAttribute('hidden');
                                novoClone.querySelector('#infoRetirado').setAttribute('hidden', '');
                            }
                        }
                    }

                    novoClone.querySelector('#preco').textContent = dados.Valor;

                    elem.append(novoClone);
                })

                $('#num' + tag.id).text(' - ' + data.length);
            },
            error(e) {
                console.error('erro');
            }
        });
    }
}

function formatarNumCelular(numCelular) {
    let novoNumCelular;
    if (numCelular.length == 11) {
        novoNumCelular = ['(', numCelular.slice(0, 2), ') ', numCelular.slice(2, 7), '-', numCelular.slice(7)].join('');
    } else {
        novoNumCelular = [numCelular.slice(0, 6), '-', numCelular(6)].join('');
    }

    return novoNumCelular;
}

function acaoPedido(idPedido, acao, num) {

    $.ajax({
        url: '/Pedido/set' + acao + 'Pedido',
        data: { idPedido: idPedido },
        dataType: 'json',
        method: 'get',
        success(data) {
            if (data == 'Sucesso') {
                if (acao == 'Entregar') {
                    let tag = $('span[btnpedido=\'' + idPedido + '\']').next();
                    $('span[btnpedido=\'' + idPedido + '\']').remove();
                    tag.fadeIn('slow');
                } else {
                    $('div[pedido="' + idPedido + '"]').fadeOut();
                    $('.pdd' + acao).fadeToggle();
                    setTimeout($('.pdd' + acao).fadeToggle()
                        , 3000);
                    $('#num' + num).text(' - ' + parseInt($('#num' + num).text().replace(' - ', '')) - 1);
                }
            } else {
                $('.pddErro').fadeToggle();
                setTimeout($('.pddErro').fadeToggle()
                    , 4000);
            }
        },
        erro(e) {
            $('.pddErro').fadeToggle();
            setTimeout($('.pddErro').fadeToggle()
                , 4000);
            console.log(e);
        }
    });
}