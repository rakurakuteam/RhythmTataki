<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateUserSongsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('user_songs', function (Blueprint $table) {
            $table->increments('id');
            $table->unsignedInteger('file_id')->commit('파일번호');
            $table->foreign('file_id')->references('id')->on('files')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->string('name', 100);
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('user_songs', function(Blueprint $table){
            $table->dropForeign('user_songs_file_id_foreign');
        });
        Schema::dropIfExists('user_songs');
    }
}
