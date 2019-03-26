<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateAddressesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('addresses', function (Blueprint $table) {
            $table->increments('id');
            $table->integer('zip_code')->comment('우편번호');
            $table->string('addr_1', 100)->comment('주소');
            $table->string('addr_2', 100)->comment('상세');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('addresses', function(Blueprint $table){
            $table->dropForeign('addresses_user_id_foreign');
        });
        Schema::dropIfExists('addresses');
    }
}
