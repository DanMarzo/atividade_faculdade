$(window).on("pageshow", () => {
    setLoading(false);
});


function setLoading(isLoading) {
    if (isLoading)
        $('#loading').fadeIn(200);
    else
        $('#loading').fadeOut(200);
}


$('#cep').on("input", async (e) => {
    let cep = $(e.target).val();
    if (cep) {
        cep = cep.replaceAll("-", "");
        let totalChar = cep.length;

        if (totalChar === 8)
            if (cep) {
                try {
                    setLoading(true);
                    let response = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
                    if (response.ok) {
                        let endereco = await response.json();
                        console.log(endereco);
                        console.log(`CEP ${cep}`);
                        $("[data-form]").each(function () {
                            let nameInput = $(this).attr('data-form'); // pega o valor do atributo

                            Object.keys(endereco).forEach(key => {
                                if (key === nameInput) {
                                    $(this).val(endereco[key]);
                                }
                            });
                        });
                    }
                    setLoading(false);
                } catch (e) {
                    console.log(e);
                    setLoading(false);
                    
                }

            }
    }
});
