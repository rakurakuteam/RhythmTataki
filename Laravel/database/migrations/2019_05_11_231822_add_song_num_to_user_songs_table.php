<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AddSongNumToUserSongsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('user_songs', function (Blueprint $table) {
            $table->unsignedInteger('song_num')->after('user_id')->commnt('노래번호');
            
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('user_songs', function (Blueprint $table) {
            $table->dropColumn('song_num');
        });
    }
}
