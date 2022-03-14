<script setup>
    import { computed, toRefs } from 'vue';
    import { useStore } from 'vuex';

    const store = useStore();

    const props = defineProps({
        sets: {
            type: Array,
            required: true
        }
    });
    const { sets } = toRefs(props);   

    //--- get exercises (name) for exerciseIds in all sets
    const viewSets = computed(() => {
        const tempViewSets = [];
        for (const set of sets.value) {
            const tempExercise = computed(() => store.getters.getExerciseById(set.exerciseId));
            tempViewSets.push({ ...set, exerciseName: tempExercise.value.name });
        }
        return tempViewSets;
    });

</script>

<template>
    <div>
        <header>
            <b>List of sets:</b>
        </header>
        <main>

            <ul>
                <li v-for="viewSet in viewSets" v-bind:key="viewSet.id">
                    <b>{{ viewSet.exerciseRepetitions }}</b> repetitions of <b>{{ viewSet.exerciseName }}</b>
                </li>
            </ul>
        </main>
    </div>
</template>

<style scoped>

</style>
