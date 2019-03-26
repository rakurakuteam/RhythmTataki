<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateOrdersTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('orders', function (Blueprint $table) {
            $table->integer('id')->comment('주문번호');
            $table->unsignedInteger('product_id')->comment('상품번호');
            $table->foreign('product_id')->references('id')->on('products')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->integer('quantity')->comment('수량');
            $table->unsignedInteger('addr_id')->comment('배송지');
            $table->foreign('addr_id')->references('id')->on('addresses')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->string('request', 255)->nullable()->comment('요청사항');
            $table->unsignedInteger('status_id')->comment('배송상태');
            $table->foreign('status_id')->references('id')->on('delivery_statuses')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->string('payment', 20)->comment('결제수단');
            $table->timestamps();   //주문일시, 요청일
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('orders', function (Blueprint $table) {
            $table->dropForeign('orders_product_id_foreign');
            $table->dropForeign('orders_addr_id_foreign');
            $table->dropForeign('orders_status_id_foreign');
        });
        Schema::dropIfExists('orders');
    }
}
