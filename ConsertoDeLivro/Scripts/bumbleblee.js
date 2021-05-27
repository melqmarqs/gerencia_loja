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
const recusar = 'Recusar';
const apagar = 'Apagar';
let div;

$('document').ready(function () {
    if ($('.avaliacao').length > 0) {
        div = $('.avaliacao [hidden]').first().clone();
        $('.avaliacao [hidden]').first().removeClass('io');

        $.ajax({
            url: '/Pedido/getUltimasAvaliacoes',
            dataType: 'json',
            method: 'get',
            success(data) {
                $.each(data, function (index, dados) {
                    if (index > 15) {
                        console.log('Há mais de 15 avaliações, mas estamos mostrando as 15 mais recentes.');
                    } else {
                        let clone = div.clone();
                        clone.attr('pedido', dados.PedidoID);
                        clone.removeAttr('hidden');
                        clone.addClass(dados.Avaliacao == 1 ? 'pessimo' : dados.Avaliacao >= 2 && dados.Avaliacao <= 4 ? 'ruim' : dados.Avaliacao == 5 || dados.Avaliacao == 6 ? 'mediano' : dados.Avaliacao == 7 || dados.Avaliacao == 8 ? 'bom' : dados.Avaliacao == 9 ? 'muito-bom' : 'otimo');
                        clone.find('#nota').first().text(dados.Avaliacao);
                        clone.find('#ultimoNome').first().text(dados.Usuario.UltimoNome);
                        clone.find('#dataPdd').first().text(dados.DataPedido);
                        clone.find('#comentario').first().text(dados.Comentario);

                        $('.avaliacao').append(clone);
                    }
                });

                $('[hidden]').removeClass('io');
            },
            error(e) {
                console.log(e);
            }
        })
    }
});

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
                        location.href = '/ConsertoDeLivro/Index'; //Navegando para outra pagina do site
                    } else {
                        window.location.href = '/ConsertoDeLivro/Login'; //Navegando para a pagina de Login
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

    if (((cod < 48 || cod > 57) && (cod < 96 || cod > 105)) && //numeros
        (cod < 37 || cod > 40) && //arrows
        cod != 8) { //backspace
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

function numeroDecimal(tag, key) {

    let cod = key.keyCode || key.which;
    let tam = tag.value.includes(',') ? 3 : 2;

    if (tag.value.includes(',') && tag.value.split(',')[1].length >= 1) {
        key.preventDefault();
        return true;
    }

    if (cod != 44) {
        if (((cod < 48 || cod > 57) && (cod < 96 || cod > 105)) || tag.value.length > tam) {
            key.preventDefault();
        }
    } else if (tag.value.includes(',') || tag.value == '' || tag.value.length >= 3) {
        key.preventDefault();
    }
}

function precoMaterial(tag, key) {
    larg = tag.id == 'Largura' ? tag.value.replace(',', '.') : larg;
    alt = tag.id == 'Altura' ? tag.value.replace(',', '.') : alt;
    exp = tag.id == 'Expessura' ? tag.value.replace(',', '.') : exp;

    larg = parseFloat(larg) || parseFloat($('#Largura').val().replace(',','.'));
    alt = parseFloat(alt) || parseFloat($('#Altura').val().replace(',', '.'));
    exp = parseFloat(exp) || parseFloat($('#Expessura').val().replace(',', '.'));

    if (larg > 0 && alt > 0 && exp > 0) {
        let matId = $('select[name="MaterialID"]')[0].value;
        $.ajax({
            url: '/Material/getMaterialById',
            method: 'get',
            dataType: 'json',
            data: { id: matId },
            success(data) {
                if (data == 3) { //3 - Falha - nesse caso, é o material que nao foi encontrado no bando de dados
                    console.log('Esse material não foi encontrado no sistema!');
                    alert('Desculpa!\nEstamos com um problem interno. Tente novamente mais tarde.');
                } else {
                    valor = parseFloat(data.ValorMetro);
                    calculaValor(valor);
                }
            },
            erro(e) {
                console.log(e);
                alert('Desculpa!\nEstamos com um problem interno. Tente novamente mais tarde.');
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
    let elem = $('#' + tag.id).next();
    if (elem.css('display') == 'block') {
        elem.fadeOut();

        elem.children('.semPedidos').css('display', 'none');

        elem.children().each(function (index, div) {
            if (div.getAttribute('pedido')) { //Selecionando todos os pedidos que foram carregados em consultas anteriores
                div.remove(); //Removendo as divs
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
                if (data.length > 0) {
                    data.forEach(function (dados, index) {
                        if (index <= 20) {
                            let clone = elem.children('.pdd').clone();
                            clone[0].removeAttribute('hidden');
                            let novoClone = clone[0];

                            novoClone.setAttribute('pedido', dados.PedidoID);

                            novoClone.querySelector('#idPedido').textContent = dados.PedidoID;

                            if (novoClone.querySelector('#cliente'))
                                novoClone.querySelector('#cliente').textContent = dados.Usuario.Nome + ' ' + dados.Usuario.UltimoNome;

                            if (novoClone.querySelector('#numCelular')) {
                                let numCelular = formatarNumCelular(dados.Usuario.Celular);
                                novoClone.querySelector('#numCelular').textContent = numCelular;
                            }

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

                            if (novoClone.querySelector('#btnRecusar'))
                                novoClone.querySelector('#btnRecusar').setAttribute('onclick', 'acaoPedido(' + dados.PedidoID + ', \'' + recusar + '\', \'' + tag.id + '\')');

                            if (novoClone.querySelector('#btnApagar'))
                                novoClone.querySelector('#btnApagar').setAttribute('onclick', 'acaoPedido(' + dados.PedidoID + ', \'' + apagar + '\', \'' + tag.id + '\')');

                            if (dados.Entregue) {
                                //vira aqui caso o usuario for ADM
                                if (novoClone.querySelector('#infoEntregue') && novoClone.querySelector('#btnEntregar')) {
                                    novoClone.querySelector('#btnEntregar').setAttribute('hidden', '');
                                    novoClone.querySelector('#btnEntregar').removeAttribute('class');
                                    novoClone.querySelector('#infoEntregue').removeAttribute('hidden');
                                }

                                //vira aqui se o usuario nao for ADM
                                if (novoClone.querySelector('#infoRetirar') && novoClone.querySelector('#infoRetirado')) {
                                    novoClone.querySelector('#infoRetirar').setAttribute('hidden', '');
                                    novoClone.querySelector('#infoRetirado').removeAttribute('hidden');
                                }

                                if (novoClone.querySelector('#avaliarPedido') && novoClone.querySelector('#nota')) {
                                    if (dados.Avaliacao != 0) {
                                        novoClone.querySelector('#nota').removeAttribute('hidden');
                                        novoClone.querySelector('#nota').setAttribute('class', dados.Avaliacao == 1 ? 'pessimo' : dados.Avaliacao >= 2 && dados.Avaliacao <= 4 ? 'ruim' : dados.Avaliacao == 5 || dados.Avaliacao == 6 ? 'mediano' : dados.Avaliacao == 7 || dados.Avaliacao == 8 ? 'bom' : dados.Avaliacao == 9 ? 'muito-bom' : 'otimo');
                                        //novoClone.querySelector('#nota').textContent = dados.Avaliacao;
                                        novoClone.querySelector('#nota').innerHTML = '<span class="glyphicon glyphicon-star"></span> ' + dados.Avaliacao;
                                    } else {
                                        novoClone.querySelector('#avaliarPedido').setAttribute('onclick', 'avaliacao(false, ' + dados.PedidoID + ')');
                                        novoClone.querySelector('#avaliarPedido').setAttribute('class', 'btn btn-primary btn-xs');
                                        novoClone.querySelector('#avaliarPedido').removeAttribute('hidden');
                                    }
                                }
                            } else {
                                //vira aqui caso o usuario for ADM
                                if (novoClone.querySelector('#infoEntregue') && novoClone.querySelector('#btnEntregar')) {
                                    novoClone.querySelector('#infoEntregue').setAttribute('hidden', '');
                                    novoClone.querySelector('#btnEntregar').removeAttribute('hidden');
                                    novoClone.querySelector('#btnEntregar').setAttribute('btnpedido', dados.PedidoID);
                                    novoClone.querySelector('#btnEntregar').setAttribute('onclick', 'acaoPedido(' + dados.PedidoID + ', \'' + entregar + '\', \'' + tag.id + '\')');
                                }

                                //vira aqui se o usuario nao for ADM
                                if (novoClone.querySelector('#infoRetirar') && novoClone.querySelector('#infoRetirado')) {
                                    novoClone.querySelector('#infoRetirar').removeAttribute('hidden');
                                    novoClone.querySelector('#infoRetirado').setAttribute('hidden', '');
                                }
                            }

                            novoClone.querySelector('#preco').textContent = dados.Valor;

                            if (novoClone.querySelector('#detalhesPdd'))
                                novoClone.querySelector('#detalhesPdd').setAttribute('href', '/Pedido/DetalhesPedido?idPedido=' + dados.PedidoID);

                            elem.append(novoClone);
                        }
                    })

                    $('#num' + tag.id).text(' - ' + data.length);
                } else {
                    elem.children('.semPedidos').fadeToggle();
                }
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
        url: '/Pedido/set' + acao + 'Pedido', //A propriedade ACAO poderá ser varios valores, que sao correspondentes com as funções e status
        data: { idPedido: idPedido },
        dataType: 'json',
        method: 'get',
        success(data) {
            if (data == 1) { //1 = Sucesso
                if (acao == 'Entregar') {
                    let labelEntregue = $('span[btnpedido=\'' + idPedido + '\']').next(); //Guardando a label entregue para habilita-la na sequencia
                    $('span[btnpedido=\'' + idPedido + '\']').remove(); //Removendo o botão entregar, pois o status do pedido ja foi mudando
                    labelEntregue.fadeIn('slow');
                } else {
                    $('div[pedido="' + idPedido + '"]').fadeOut(); //Escondendo a div do pedido que o status foi alterado
                    $('.pdd' + acao).fadeToggle(); //Exibindo a msg da ação feita
                    setTimeout(function () { $('.pdd' + acao).fadeToggle() }
                        , 3000);
                    $('#num' + num).text(' - ' + (parseInt($('#num' + num).text().replace(' - ', '')) - 1)); //Alterando a contagem de pedidos
                }
            } else {
                //$('.pddErro').fadeToggle();
                //setTimeout(function () { $('.pddErro').fadeToggle() }
                //    , 4000);
                alert(data);
            }
        },
        erro(e) {
            $('.pddErro').fadeToggle();
            setTimeout(function () { $('.pddErro').fadeToggle() }
                , 4000);
            console.log(e);
        }
    });

}

function avaliacao(fechar, idPedido) {
    if (fechar) {
        //organizar valores de avaliação
        $.each($('[onclick="selecionaNum(this)"]')[0].parentElement.children, function (index, elem) {
            elem.textContent = index + 1;
        });

        $('#idPedido').text(''); //Limpando o elem idPedido
        $('#comentario').val(''); //Limpando o comentario

        $('.mAvaliar').fadeOut();
        $('html, body').css('overflow', 'auto');

        $.each($('[selecionado]').parent().children(), function (index, elem) {
            elem.style = 'opacity: 1'; //Voltando todos as notas para a opacidade 1
        });

        $('[selecionado]').removeAttr('selecionado'); //Removendo o atributo selecionado

    } else {

        $.ajax({
            url: '/Pedido/getPedidoById',
            data: { idPedido: idPedido },
            method: 'get',
            dataType: 'json',
            success(data) {
                if (data != 3) { //3 = Falha - nesse caso, é pq nao foi encontrado nenhum pedido com o id passado
                    $('#idPedido').text(idPedido);
                    $('#livro').text(data.NomeLivro);
                    $('#autor').text(data.AutorLivro);

                    $('.mAvaliar').fadeIn();
                    $('html, body').css('overflow', 'hidden'); //Escondendo o scroll da tela, para ficar fixada no 'popup'

                } else {
                    console.log('Não foi encontrado nenhum pedido com o ID informado. ID ', idPedido);
                    alert('Atenção\nTivemos um problema interno, por favor, tenta novamente mais tarde.');
                    $('.pddErro').fadeToggle();
                    setTimeout(function () {
                        $('.pddErro').fadeToggle();
                    }, 4000);
                }
            },
            error(e) {
                console.log('Houve um erro de comunicação.');
                $('.pddErro').fadeToggle();
                setTimeout(function () {
                    $('.pddErro').fadeToggle();
                }, 5000);

                console.log(e.statusText);
            }
        });
    }
}

function selecionaNum(tag) {
    $.each(tag.parentNode.children, function (index, elem) {
        if (tag == elem) {
            elem.style = 'opacity: 1'; //Voltando a opacidade da nota selecionada para 1 (normal)
            elem.setAttribute('selecionado', ''); //Atibuindo o atributo selecionado para eu saber qual nota foi selecionada
        } else {
            elem.style = 'opacity: 0.5';
            elem.removeAttribute('selecionado');
        }
    });
}

function salvarAvaliacao() {
    //pegar o valor avaliado - se nao tiver sido atribuido nenhum valor, sera null
    let numAvaliacao = $('.mAvaliar [selecionado]').length > 0 ? parseInt($('.mAvaliar [selecionado]').first().text()) : null;

    if (numAvaliacao == null) {
        alert('Atenção\nNão foi possivel salvar as informações, pois você não deu nota ao serviço.');
    } else {
        if (isNaN(numAvaliacao)) { //vira aqui caso o valor avaliado n seja numerico
            avaliacao(true);

            $('.pddErro').fadeToggle();
            setTimeout(function () {
                $('.pddErro').fadeToggle();
            }, 5000);
        } else {
            numAvaliacao = numAvaliacao > 10 ? 10 : numAvaliacao < 1 ? 1 : numAvaliacao; //restingindo o valor para no min 0 e no max 10, sendo todos valores inteiros
            let id = parseInt($('#idPedido').text());
            let coment = $('#comentario').val();

            if (!isNaN(id)) {
                $.ajax({
                    url: '/Pedido/setAvaliacaoEComentario',
                    data: { idPedido: id, avaliacao: numAvaliacao, comentario: coment },
                    dataType: 'json',
                    method: 'post',
                    success(data) {
                        avaliacao(true);
                        if (data == 1) { //1 = Sucesso
                            //Exibindo a msg de avaliado
                            $('.pddAvaliar').fadeToggle();
                            setTimeout(function () {
                                $('.pddAvaliar').fadeToggle()
                            }, 4000);

                            //Escondendo o botão de avaliar
                            $('[pedido="' + id + '"] #avaliarPedido').first().fadeOut();

                            $('[pedido="' + id + '"] #nota').first().addClass(numAvaliacao == 1 ? 'pessimo' : numAvaliacao >= 2 && numAvaliacao <= 4 ? 'ruim' : numAvaliacao == 5 || numAvaliacao == 6 ? 'mediano' : numAvaliacao == 7 || numAvaliacao == 8 ? 'bom' : numAvaliacao == 9 ? 'muito-bom' : 'otimo');
                            $('[pedido="' + id + '"] #nota').first().html('<span class="glyphicon glyphicon-star"></span> ' + numAvaliacao);
                            $('[pedido="' + id + '"] #nota').first().fadeIn();
                            $('[pedido="' + id + '"] #nota').first().removeAttr('hidden');
                        } else {
                            console.log(data);
                            $('.pddErro').fadeToggle();
                            setTimeout(function () {
                                $('.pddErro').fadeToggle();
                            }, 5000);
                        }
                    },
                    error(e) {
                        avaliacao(true);
                        console.log(e);
                        $('.pddErro').fadeToggle();
                        setTimeout(function () {
                            $('.pddErro').fadeToggle();
                        }, 5000);
                    }
                });
            }
        }
    }
}

function minhaFunc(tag) {
    if (!tag.hasAttribute('selecionado')) {
        $.each(tag.parentElement.children, function (index, elem) {
            if (elem.hasAttribute('selecionado')) {
                elem.removeAttribute('selecionado');
            } else if (elem == tag) {
                elem.setAttribute('selecionado', '');
            }
        });

        $('.avaliacao > [pedido]').remove();

        $.ajax({
            url: '/Pedido/get' + tag.id,
            dataType: 'json',
            method: 'get',
            success(data) {
                if (data.length > 0) {
                    $.each(data, function (index, pedido) {
                        if (index < 15) {
                            let novaDiv = div.clone();
                            novaDiv.removeAttr('hidden');
                            novaDiv.attr('pedido', pedido.PedidoID);
                            novaDiv.addClass(pedido.Avaliacao == 1 ? 'pessimo' : pedido.Avaliacao >= 2 && pedido.Avaliacao <= 4 ? 'ruim' : pedido.Avaliacao == 5 || pedido.Avaliacao == 6 ? 'mediano' : pedido.Avaliacao == 7 || pedido.Avaliacao == 8 ? 'bom' : pedido.Avaliacao == 9 ? 'muito-bom' : 'otimo');

                            novaDiv.find('#nota').first().text(pedido.Avaliacao);
                            novaDiv.find('#ultimoNome').first().text(pedido.Usuario.UltimoNome);
                            novaDiv.find('#dataPdd').first().text(pedido.DataPedido);
                            novaDiv.find('#comentario').first().text(pedido.Comentario);

                            $('.avaliacao').append(novaDiv);
                        }
                    });
                }
            }
        })
    }
}