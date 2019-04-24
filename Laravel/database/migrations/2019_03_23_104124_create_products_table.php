<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateProductsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('products', function (Blueprint $table) {
            $table->increments('id')->comment('상품번호');
            $table->string('name', 50)->comment('상품이름');
            $table->integer('price')->comment('상품가격');
            $table->integer('stock')->comment('상품재고');
            $table->text('content')->comment('상품내용');
            $table->timestamps();   // 등록일, 수정일
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('products');
    }
}
