<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateUserAddrsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('user_addrs', function (Blueprint $table) {
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
        Schema::table('user_addrs', function(Blueprint $table){
            $table->dropForeign('user_addrs_user_id_foreign');
            $table->dropForeign('user_addrs_addr_id_foreign');
        });
        Schema::dropIfExists('user_addrs');
    }
}
