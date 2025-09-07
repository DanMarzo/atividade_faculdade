$("#signo-form").on("submit", async (event) => {
    event.preventDefault();

    let formulario = new FormData(event.target);

    let dataNascimento = formulario.get('data_nascimento');

    if (dataNascimento) {
        event.target.submit();
        return
    }
    return
});