$("#enviar").click(function () {
    var dados = {
        txtNome: $("#txtNome").val(),
        txtEmail: $("#txtEmail").val(),
        txtMensagem: $("#txtMensagem").val(),
        txtWebsite: $("#txtWebsite").val()
    };
    $.ajax({
        url: '@Url.Action("SendEmail", "Home")',
        type: 'POST',
        data: dados,
        success: function (response) {
            alert('Um email foi enviado com sucesso');
        },
        error: function () {
            // Lógica para lidar com erros
            console.log("Erro ao enviar dados.");
        }
    });
});