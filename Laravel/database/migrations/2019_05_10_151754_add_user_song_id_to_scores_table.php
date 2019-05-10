<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AddUserSongIdToScoresTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('scores', function (Blueprint $table) {
            $table->unsignedInteger('song_id')->nullable()->change();
            $table->unsignedInteger('user_songs_id')->nullable()->commit('사용자 노래 번호')->after('song_id');
            $table->foreign('user_songs_id')->references('id')->on('user_songs')
                    ->onUpdate('cascade')->onDelete('cascade');;
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('scores', function (Blueprint $table) {
            $table->dropForeign('scores_user_songs_id_foreign');
            $table->dropColumn('user_songs_id');
            $table->unsignedInteger('user_songs_id')->change();
        });

    }
}
