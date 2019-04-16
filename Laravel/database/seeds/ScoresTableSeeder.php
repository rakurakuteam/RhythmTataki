<?php

use Illuminate\Database\Seeder;

class ScoresTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run(){
        for($i=1; $i<=11; $i++){
            DB::table('scores')->insert([
                'user_id' => 25,
                'song_id' => $i,
                'score' => 0,
                'created_at' => now()
            ]);
        }
    }
}
