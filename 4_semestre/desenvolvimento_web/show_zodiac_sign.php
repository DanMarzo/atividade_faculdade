<?php

$data_nascimento = $_POST['data_nascimento']; // YYYY-MM-DD
$signos = simplexml_load_file("signos.xml");

// Obter dia e mes
list($ano, $mes, $dia) = explode("-", $data_nascimento);
$dataFormatada = sprintf("%02d-%02d", $dia, $mes); // 07-09 (dd-mm)

// Data
$dataNasc = strtotime($dataFormatada);

$signoEncontrado = null;

foreach ($signos->signo as $s) {
    // pega datas inicio/fim no formato dd/mm do XML
    $inicio = DateTime::createFromFormat("d/m", (string) $s->dataInicio)->getTimestamp();
    $fim = DateTime::createFromFormat("d/m", (string) $s->dataFim)->getTimestamp();
    $data = DateTime::createFromFormat("d-m", $dataFormatada)->getTimestamp();

    // Para signos que cruzam o ano
    if ($inicio <= $fim) {
        // intervalo normal
        if ($data >= $inicio && $data <= $fim) {
            $signoEncontrado = $s;
            break;
        }
    } else {
        // intervalo que cruza o ano
        if ($data >= $inicio || $data <= $fim) {
            $signoEncontrado = $s;
            break;
        }
    }
}

?>

<!DOCTYPE html>
<html lang="pt-br">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="/assets/css/style.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">

    <title>Zodi fake</title>
</head>


<body>
    <?php include('layouts/header.php'); ?>
    <div class="row justify-content-center">
        <div class="col-8 bg-primary-app rounded">
            <?php if ($signoEncontrado != null): ?>
                <h2 class="text-center text-white"><?= $signoEncontrado->signoNome ?></h2>
                <p class="text-white"><?= $signoEncontrado->descricao ?></p>

            <?php else: ?>
                <p>Não foi possível determinar o signo.</p>
            <?php endif; ?>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"
        integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.min.js"
        integrity="sha384-G/EV+4j2dNv+tEPo3++6LCgdCROaejBqfUeNjuKAiuXbjrxilcCdDz6ZAVfHWe1Y"
        crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"
        integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
    <script src="./assets/js/index.js?v=2"></script>
</body>

</html>