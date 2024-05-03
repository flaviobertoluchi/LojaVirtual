﻿$(function () {
    $('.qtdMenos').on('click', function (e) {
        e.preventDefault();
        alterarQtd(parseInt($('.qtd').val()) - 1);
    });

    $('.qtdMais').on('click', function (e) {
        e.preventDefault();
        alterarQtd(parseInt($('.qtd').val()) + 1);
    });

    $('.qtd').on('change', function () {
        alterarQtd(parseInt($(this).val()));
    });

    $('.qtd').on('keypress', function (e) {
        if (e.which == '13') {
            e.preventDefault();
            alterarQtd(parseInt($(this).val()));
        }
    });

    function alterarQtd(qtd) {
        let qtdInput = $('.qtd');
        let estoque = parseInt($('.estoque').val());
        if (isNaN(qtd)) {
            qtd = 1;
        }
        else if (qtd < 1) {
            qtd = 1;
        }
        else if (qtd > estoque) {
            qtdInput.popover('show');
            setTimeout(function () {
                qtdInput.popover('hide');
            }, 1500);
            qtd = estoque;
        }
        qtdInput.val(qtd);
    }

    $('.imagem').on('click', function () {
        $('.imagemPrincipal').attr('src', $(this).attr('src'));
    });
});
