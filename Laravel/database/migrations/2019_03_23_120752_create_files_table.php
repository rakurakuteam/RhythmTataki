<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateFilesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('files', function (Blueprint $table) {
            $table->increments('id');
            $table->unsignedInteger('board_id')->comment('게시글번호');
            $table->foreign('board_id')->references('id')->on('boards')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->string('route', 255)->comment('파일경로');
            $table->string('name', 255)->comment('파일이름');
            $table->string('type', 20)->comment('파일타입');
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
        Schema::table('files', function(Blueprint $table){
            $table->dropForeign('files_board_id_foreign');
        });
        Schema::dropIfExists('files');
    }
}
