package com.marzo.hamburgueriaz;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

public class MainActivity extends AppCompatActivity {
    private TextView tvQuantidade;
    private int quantidade = 0; // Variável de controle numérica

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

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
    }

    private void incrementarItem() {
        quantidade++;
        tvQuantidade.setText(String.valueOf(quantidade));
    }
    private void decrementarItem() {

        if(quantidade == 0)
            return;

        quantidade--;
        tvQuantidade.setText(String.valueOf(quantidade));
    }
}