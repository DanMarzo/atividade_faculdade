package com.marzo.hamburgueriaz;

import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.LinearLayout;
import android.widget.TextView;

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
    private int quantidade = 0; // Variável de controle numérica

    private List<AcompanhamentoModel> acompanhamentos = new ArrayList<>();

    private void seedtAcompanhamentos() {

        this.acompanhamentos.add(new AcompanhamentoModel(1, 2, "Bacon"));
        this.acompanhamentos.add(new AcompanhamentoModel(2, 2, "Queijo"));
        this.acompanhamentos.add(new AcompanhamentoModel(3, 3, "Onion Rings"));
    }

    private float totalPrice = 0;

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
        for (AcompanhamentoModel item : this.acompanhamentos) {
            // Este mano cria a instancia do CheckBox, parece com Windows Forms
            CheckBox cb = new CheckBox(this);

            cb.setText(item.getName());
            cb.setTag(item.getId());
            cb.setLayoutParams(new LinearLayout.LayoutParams(
                    LinearLayout.LayoutParams.WRAP_CONTENT,
                    LinearLayout.LayoutParams.WRAP_CONTENT
            ));
            // Mano que horrivel ter que fazer isso toda hora hahah
            cb.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                    int idRecuperado = Integer.parseInt(buttonView.getTag().toString());

                    AcompanhamentoModel acompanhamento = acompanhamentos
                            .stream()
                            .filter(X -> X.getId() == idRecuperado)
                            .findFirst()
                            .orElse(null);

                    if (acompanhamento == null) {
                        Log.d("PEDIDO", "Selecionou o ID: " + idRecuperado + "E nao foi encontrado, mas como e so tarefa da facul nao vou mais tratar");
                        return;
                    }

                    if (isChecked) {
                        Log.d("PEDIDO", "Selecionou o ID: " + idRecuperado);
                        totalPrice += acompanhamento.getPrice();
                    } else {
                        Log.d("PEDIDO", "Desmarcou o ID: " + idRecuperado);
                        totalPrice -= acompanhamento.getPrice();
                    }

                    Log.d("PEDIDO", "Desmarcou o ID: " + idRecuperado + "Total " + totalPrice);
                }
            });

            // Termina de incluir na View
            container.addView(cb);
        }
    }

    private void incrementarItem() {
        quantidade++;
        tvQuantidade.setText(String.valueOf(quantidade));
    }

    private void decrementarItem() {

        if (quantidade == 0)
            return;
        quantidade--;
        tvQuantidade.setText(String.valueOf(quantidade));
    }
}