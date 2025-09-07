let btnSenha = $("#btn-handle-senha");
let invisivel = "invisivel";
let visivel = "visivel";

function start() {
    let tipo = btnSenha.data("tipo-input");
    setTipoInput(tipo);
}

function setTipoInput(tipo) {
    if (tipo === invisivel) {
        btnSenha.html('<i class="bi bi-eye-slash"></i>');
    } else {
        btnSenha.html('<i class="bi bi-eye-fill"></i>');

    }
    btnSenha.data("tipo-input", tipo);
}

btnSenha.on("click", function (e) {
    let tipo = $(this).data("tipo-input");
    console.log(tipo);
    let isVisible = tipo === invisivel;
    setTipoInput(isVisible ? visivel : invisivel);
    $("#senha").attr("type", isVisible ? "text" : "password");
});

start();
