package com.marzo.hamburgueriaz;

import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.marzo.hamburgueriaz.models.AcompanhamentoModel;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {
    private TextView tvQuantidade;
    private TextView resumo;
    private int quantidade = 0; // Variável de controle numérica
    private List<AcompanhamentoModel> acompanhamentos = new ArrayList<>();

    private void seedtAcompanhamentos() {
        this.acompanhamentos.add(new AcompanhamentoModel(1, 2, "Bacon"));
        this.acompanhamentos.add(new AcompanhamentoModel(2, 2, "Queijo"));
        this.acompanhamentos.add(new AcompanhamentoModel(3, 3, "Onion Rings"));
    }

    private float totalPriceAcompanhamentos = 0;
    private float totalPriceComQtde = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.seedtAcompanhamentos();
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        tvQuantidade = findViewById(R.id.txtQuantidade);
        resumo = findViewById(R.id.txtPreco);
        Button btnIncrementar = findViewById(R.id.btn_plus);
        Button btnDecrementar = findViewById(R.id.btn_down);

        btnIncrementar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                incrementarItem();
            }
        });
        btnDecrementar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                decrementarItem();
            }
        });

        //Nao sei se é uma boa pratica fazer isso nesse override, mas nao sou especialista mesmo
        LinearLayout container = findViewById(R.id.pedidos_check_itens);
        // Usando o for clássico com índice i
        for (int i = 0; i < this.acompanhamentos.size(); i++) {
            // Recupera o item da lista pela posição (Índice)
            AcompanhamentoModel item = this.acompanhamentos.get(i);

            CheckBox cb = new CheckBox(this);

            // Configurações básicas
            cb.setText(item.getName());
            cb.setTag(item.getId()); // ID da sua aplicação/banco

            // Define os parâmetros de layout (Equivalente ao Width/Height do Windows Forms)
            cb.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WRAP_CONTENT, LinearLayout.LayoutParams.WRAP_CONTENT));

            // Evento de clique usando Lambda (disponível no seu Java 11)
            cb.setOnCheckedChangeListener((buttonView, isChecked) -> {
                // Aqui você chama o seu método de processamento
                onCheckedItensAcompanhamentos(buttonView, isChecked);
            });

            // Adiciona o CheckBox criado ao seu LinearLayout do XML
            container.addView(cb);
        }

        Button fazerPedidoBtn = findViewById(R.id.fazer_pedido_btn);

        fazerPedidoBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                enviarPedido();
            }
        });
    }

    private void onCheckedItensAcompanhamentos(CompoundButton buttonView, boolean isChecked) {
        int idRecuperado = Integer.parseInt(buttonView.getTag().toString());

        AcompanhamentoModel acompanhamento = acompanhamentos.stream().filter(X -> X.getId() == idRecuperado).findFirst().orElse(null);

        if (acompanhamento == null) {
            Log.d("PEDIDO", "Selecionou o ID: " + idRecuperado + "E nao foi encontrado, mas como e so tarefa da facul nao vou mais tratar");
            return;
        }

        if (isChecked) {
            Log.d("PEDIDO", "Selecionou o ID: " + idRecuperado);
            totalPriceAcompanhamentos += acompanhamento.getPrice();
        } else {
            Log.d("PEDIDO", "Desmarcou o ID: " + idRecuperado);
            totalPriceAcompanhamentos -= acompanhamento.getPrice();
        }

        acompanhamento.setSelecionado(isChecked);

        if (quantidade == 0) return;
        totalPriceComQtde = quantidade * totalPriceAcompanhamentos;
        resumo.setText(String.valueOf(totalPriceComQtde));
        Log.d("PEDIDO", "Desmarcou o ID: " + idRecuperado + "Total " + totalPriceAcompanhamentos);
    }

    private void incrementarItem() {
        quantidade++;
        tvQuantidade.setText(String.valueOf(quantidade));

        if (totalPriceAcompanhamentos > 0) {
            totalPriceComQtde = quantidade * totalPriceAcompanhamentos;
            resumo.setText(String.valueOf(totalPriceComQtde));
        }
    }

    private void decrementarItem() {
        if (quantidade == 0) return;

        quantidade--;
        tvQuantidade.setText(String.valueOf(quantidade));

        if (totalPriceAcompanhamentos > 0) {
            totalPriceComQtde = quantidade * totalPriceAcompanhamentos;
            resumo.setText(String.valueOf(totalPriceComQtde));
        } else
            resumo.setText(String.valueOf(0));

    }

    private void enviarPedido() {
        TextView nomeUsuario = findViewById(R.id.nome_usuario);
        String nome = String.valueOf(nomeUsuario.getText());

        if (nome.isBlank()) {
            Toast.makeText(this, "Inclua seu nome", Toast.LENGTH_SHORT).show();
            return;
        }
        if (quantidade == 0 || totalPriceAcompanhamentos == 0) {
            Toast.makeText(this, "Selecione alguam coisa ne", Toast.LENGTH_SHORT).show();
            return;
        }

        float total = this.quantidade * totalPriceAcompanhamentos;
        Log.d("Pedido", "Nome " + nome + " Total " + total);

        StringBuilder resumo = new StringBuilder();
        resumo.append(String.format("%s \n", nome));

        for (AcompanhamentoModel item : acompanhamentos) {
            resumo.append(String.format("Tem %s? %s\n", item.getName(), item.isSelecionado() ? "Sim" : "Não"));
            Log.d("ITEM PEDIDO", "Nome: " + item.getName() + " Incluido: " + item.isSelecionado());
        }
        resumo.append(String.format("Quantidade: %d \n", quantidade));
        resumo.append(String.format("Preço final: R$ %.2f \n", quantidade * totalPriceAcompanhamentos));

        Intent intent = new Intent(Intent.ACTION_SENDTO);
        intent.setData(Uri.parse("mailto:"));

        intent.putExtra(Intent.EXTRA_EMAIL, new String[]{"contato@hamburgueria.com"});
        intent.putExtra(Intent.EXTRA_SUBJECT, "Novo Pedido - " + nome);
        intent.putExtra(Intent.EXTRA_TEXT, resumo.toString());
        startActivity(intent);
    }
}