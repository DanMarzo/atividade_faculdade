<%@page contentType="text/html" pageEncoding="UTF-8"%>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
        <link  rel="stylesheet" href="./assets/css/style.css?v=5"></link>
        <title>Cadastre-se</title>
    </head>
    <body>
        <!-- Endereco -->
        <div class="container text-center">
            <h3>ENDEREÇO</h3>
            <div class="row  justify-content-between">
                <!-- CEP -->
                <div class="row g-3 align-items-center col-6">
                    <div class="d-flex">
                        <label for="cep" class="col-form-label border-rounded-left bg-primary-app">CEP</label>
                        <input data-form="cep" type="text" id="cep" class="form-control " />
                    </div>
                    <div class="col-12">
                        <span id="cepHelpInline" class="form-text"></span>
                    </div>
                </div>
                <!-- Rua -->
                <div class="row g-3 align-items-center col-6">
                    <div class="d-flex">
                        <label for="rua" class="col-form-label border-rounded-left bg-primary-app">Rua</label>
                        <input data-form="logradouro" type="text" id="rua" class="form-control" />
                    </div>
                    <div class="col-12">
                        <span id="ruaHelpInline" class="form-text"></span>
                    </div>
                </div>
            </div> 
            <div class="row justify-content-between">
                <!-- Bairro -->
                <div class="row g-3 align-items-center col-4">
                    <div class="d-flex">
                        <label for="bairro" class="col-form-label border-rounded-left bg-primary-app">Bairro</label>
                        <input data-form="bairro" type="text" id="bairro" class="form-control" />
                    </div>
                    <div class="col-12">
                        <span id="bairroHelpInline" class="form-text"></span>
                    </div>
                </div>
                <!-- Cidade -->
                <div class="row g-3 align-items-center col-4">
                    <div class="d-flex">
                        <label for="cidade" class="col-form-label border-rounded-left bg-primary-app">Cidade</label>
                        <input data-form="localidade" type="text" id="cidade" class="form-control" />
                    </div>
                    <div class="col-12">
                        <span id="cidadeHelpInline" class="form-text"></span>
                    </div>
                </div>
                <!-- Estado -->
                <div class="row g-3 align-items-center col-4">
                    <div class="d-flex">
                        <label for="estado" class="col-form-label border-rounded-left bg-primary-app">Estado</label>
                        <input data-form="estado" type="text" id="estado" class="form-control" />
                    </div>
                    <div class="col-12">
                        <span id="estadoHelpInline" class="form-text"></span>
                    </div>
                </div>
            </div>
            <div class="row justify-content-between">
                <!-- Estado -->
                <div class="row g-3 align-items-center col-6">
                    <div class="d-flex">
                        <label for="numero" class="col-form-label border-rounded-left bg-primary-app">Número</label>
                        <input data-form type="text" id="numero" class="form-control" />
                    </div>
                    <div class="col-12">
                        <span id="numeroHelpInline" class="form-text"></span>
                    </div>
                </div>
                <!-- Complemento -->
                <div class="row g-3 align-items-center col-6">
                    <div class="d-flex">
                        <label for="complemento" class="col-form-label border-rounded-left bg-primary-app">Complemento</label>
                        <input data-form="complemento" type="text" id="complemento" class="form-control" />
                    </div>
                    <div class="col-12">
                        <span id="complementoHelpInline" class="form-text"></span>
                    </div>
                </div>
            </div>
            <button class="btn btn-primary">CADASTRAR</button>
        </div>
        <!-- Spinner -->
        <div id="loading">
            <div class="spinner"></div>
        </div>
    </body>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.min.js" integrity="sha384-G/EV+4j2dNv+tEPo3++6LCgdCROaejBqfUeNjuKAiuXbjrxilcCdDz6ZAVfHWe1Y" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js" integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
    <script 
        src="./assets/js/controller.js?v=5" 
    ></script>
</html>