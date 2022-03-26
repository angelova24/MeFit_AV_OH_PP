<script setup>
    import { ref, toRefs, computed } from 'vue';
    import { useStore } from 'vuex';

    import SetList from "../Sets/SetList.vue";

    const props = defineProps({
        workout: {
            type: Object,
            required: true
        }
    });
    const { workout } = toRefs(props);

    const store = useStore();

    const sets = computed(() => {
        console.log("WorkoutSets", workout.value.sets);
        const tempSets = [];
        for (const setId of workout.value.sets) {
            console.log("setId:", setId);
            const tempSet = computed(() => store.getters.getSetById(setId));
            console.log("tempSet:", tempSet.value);
            tempSets.push(tempSet.value);
        }
        console.log(tempSets);
        return tempSets;
    });
    
</script>

<template>
    <div v-if="workout !== undefined">
        <header>
            <b>{{ workout.name }}</b><br />
            (Type: {{ workout.type }})
        </header>
        <main>
            <section title="exerciseSets">
                This workout comprises the following sets of exercises:
                <SetList v-if="sets[0] !== undefined" v-bind:sets="sets"></SetList>
            </section>
        </main>
    </div>
</template>


<style scoped>
    
    header {
        font-size: larger;
        padding: 20px 10px;
    }
    main {
        display: grid;
        grid-template-columns: 1fr;
        row-gap: 20px;
        column-gap: 10px;
        align-items:stretch;
        padding: 10px;
        
    }
    section {
        border: 1px dashed;
        border-color: white;
        box-shadow: 2px 2px 2px 1px grey;
    }

</style>
