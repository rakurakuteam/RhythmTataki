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
            $table->unsignedInteger('user_id')->comment('회원번호');
            $table->foreign('user_id')->references('id')->on('users')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->unsignedInteger('song_id')->nullable()->commit('공식노래');
            $table->foreign('song_id')->references('id')->on('songs')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->unsignedInteger('file_id')->nullable()->commit('파일번호');
            $table->foreign('file_id')->references('id')->on('files')
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
        Schema::table('user_songs', function(Blueprint $table){
            $table->dropForeign('user_songs_user_id_foreign');
            $table->dropForeign('user_songs_song_id_foreign');
            $table->dropForeign('user_songs_file_id_foreign');
        });
        Schema::dropIfExists('user_songs');
    }
}
