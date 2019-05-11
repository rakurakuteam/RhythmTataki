<?php

use Illuminate\Database\Seeder;

class User_songsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=DB::table('songs')->count(); $i++){
            DB::table('user_songs')->insert([
                'user_id' => 5,
                'song_id' => $i,
                'file_id' => null,
            ]);
        };
    }
}
