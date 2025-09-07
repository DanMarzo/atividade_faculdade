<%@page contentType="text/html" pageEncoding="UTF-8"%>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css">
        <link rel="stylesheet" href="./assets/index.css">
        <title>Cadastro de usuários</title>
    </head>

    <body>
    <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
        <div class="container-fluid d-flex justify-content-center">
            <a class="navbar-brand" href="#">Formulário de Cadastro</a>
        </div>
    </nav>

    <!-- Cadastro -->
    <div class="container">
        <div class="row justify-content-between">
            <!-- Nome -->
            <div class="row align-items-center col-6">
                <div >
                    <label for="nome" class="col-form-label border-rounded-left bg-primary-app">Nome</label>
                    <input data-form="nome" type="text" id="nome" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="nomeHelpInline" class="form-text"></span>
                </div>
            </div>

            <!-- Sobrenome -->
            <div class="row align-items-center col-6">
                <div >
                    <label for="sobrenome" class="col-form-label border-rounded-left bg-primary-app">Sobrenome</label>
                    <input data-form="sobrenome" type="text" id="sobrenome" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="sobrenomeHelpInline" class="form-text"></span>
                </div>
            </div>
        </div>

        <div class="row justify-content-between">

            <!-- Email -->
            <div class="row align-items-center col-6">
                <div >
                    <label for="email" class="col-form-label border-rounded-left bg-primary-app">Email</label>
                    <input data-form="email" type="email" id="email" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="emailHelpInline" class="form-text"></span>
                </div>
            </div>

            <!-- Senha -->
            <div class="row align-items-center col-6">
                <div >
                    <label for="senha" class="col-form-label border-rounded-left bg-primary-app">Senha</label>
                    <div class="input-group mb-3">

                        <input data-form="senha" type="password" id="senha" class="form-control pe-5">

                        <button id="btn-handle-senha" data-tipo-input="invisivel" 
                                type="button"
                                class="btn position-absolute top-50 end-0 translate-middle-y me-2 p-0">
                            <i class="bi bi-eye-slash"></i>
                        </button>
                    </div>

                </div>
                <div class="col-12">
                    <span id="senhaHelpInline" class="form-text"></span>
                </div>
            </div>

        </div>
    </div>



    <!-- Endereco -->
    <div class="container">
        <div class="row">
            <h3>Endereço</h3>
        </div>
        <div class="row justify-content-between">
            <!-- CEP -->
            <div class="row col-4">
                <div >
                    <label for="cep" class="col-form-label border-rounded-left bg-primary-app">CEP</label>
                    <input data-form="cep" type="text" id="cep" class="form-control " />
                </div>
                <div class="col-12">
                    <span id="cepHelpInline" class="form-text"></span>
                </div>
            </div>

            <!-- Rua -->
            <div class="row align-items-center col-4">
                <div >
                    <label for="rua" class="col-form-label border-rounded-left bg-primary-app">Rua</label>
                    <input data-form="logradouro" type="text" id="rua" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="ruaHelpInline" class="form-text"></span>
                </div>
            </div>

            <!-- Bairro -->
            <div class="row align-items-center col-4">
                <div >
                    <label for="bairro" class="col-form-label border-rounded-left bg-primary-app">Bairro</label>
                    <input data-form="bairro" type="text" id="bairro" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="bairroHelpInline" class="form-text"></span>
                </div>
            </div>
        </div>

        <div class="row justify-content-between">

            <!-- Cidade -->
            <div class="row align-items-center col-3">
                <div >
                    <label for="cidade" class="col-form-label border-rounded-left bg-primary-app">Cidade</label>
                    <input data-form="localidade" type="text" id="cidade" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="cidadeHelpInline" class="form-text"></span>
                </div>
            </div>


            <!-- Estado -->
            <div class="row align-items-center col-3">
                <div >
                    <label for="estado" class="col-form-label border-rounded-left bg-primary-app">Estado</label>
                    <input data-form="estado" type="text" id="estado" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="estadoHelpInline" class="form-text"></span>
                </div>
            </div>
            <!-- Estado -->
            <div class="row align-items-center col-3">
                <div >
                    <label for="numero" class="col-form-label border-rounded-left bg-primary-app">Número</label>
                    <input data-form type="text" id="numero" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="numeroHelpInline" class="form-text"></span>
                </div>
            </div>

            <!-- Complemento -->
            <div class="row align-items-center col-3">
                <div >
                    <label for="complemento" class="col-form-label border-rounded-left bg-primary-app">Complemento</label>
                    <input required data-form="complemento" type="text" id="complemento" class="form-control" />
                </div>
                <div class="col-12">
                    <span id="complementoHelpInline" class="form-text"></span>
                </div>
            </div>
        </div>

        <button class="btn btn-primary btn-dark mt-4" >Verificar</button>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.min.js" integrity="sha384-G/EV+4j2dNv+tEPo3++6LCgdCROaejBqfUeNjuKAiuXbjrxilcCdDz6ZAVfHWe1Y" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js" integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
    <script src="./assets/index.js"></script>
</body>
</html>
