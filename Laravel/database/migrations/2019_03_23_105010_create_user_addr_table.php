<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateUserAddrTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('user_addr', function (Blueprint $table) {
            $table->increments('id')->comment('배송지번호');
            $table->unsignedInteger('user_id')->comment('회원번호');
            $table->foreign('user_id')->references('id')->on('users')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->unsignedInteger('addr_id')->comment('주소');
            $table->foreign('addr_id')->references('id')->on('addresses')
                    ->onUpdate('cascade')->onDelete('cascade');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('user_addr', function(Blueprint $table){
            $table->dropForeign('user_addr_user_id_foreign');
            $table->dropForeign('user_addr_addr_id_foreign');
        });
        Schema::dropIfExists('user_addr');
    }
}
