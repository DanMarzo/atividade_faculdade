package com.marzo.hamburgueriaz.models;

public class AcompanhamentoModel {
    private int id;
    private float price;
    private String name;

    //Existe overload ainda bem
    public AcompanhamentoModel(float price, String name) {
        this.price = price;
        this.name = name;
    }

    public AcompanhamentoModel(int id, float price, String name) {
        this.id = id;
        this.price = price;
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public float getPrice() {
        return price;
    }

    public void setPrice(float price) {
        this.price = price;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
