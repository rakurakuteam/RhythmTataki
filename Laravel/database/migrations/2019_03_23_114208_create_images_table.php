<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateImagesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('images', function (Blueprint $table) {
            $table->unsignedInteger('product_id')->comment('상품번호');
            $table->foreign('product_id')->references('id')->on('products')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->string('route', 255)->comment('이미지경로');
            $table->string('name', 255)->comment('이미지이름');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('images', function (Blueprint $table) {
            $table->dropForeign('images_product_id_foreign');
        });
        Schema::dropIfExists('images');
    }
}
