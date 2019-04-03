<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateBoardFilesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('board_files', function (Blueprint $table) {
            $table->increments('id');
            $table->unsignedInteger('board_id')->comment('게시글번호');
            $table->foreign('board_id')->references('id')->on('boards')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->unsignedInteger('file_id')->comment('파일번호');
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
        Schema::table('board_files', function(Blueprint $table){
            $table->dropForeign('board_files_file_id_foreign');
            $table->dropForeign('board_files_board_id_foreign');
        });
        Schema::dropIfExists('board_files');
    }
}
