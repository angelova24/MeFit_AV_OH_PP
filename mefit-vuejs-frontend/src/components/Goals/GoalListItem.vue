<script setup>
    import { computed, toRefs, ref, onMounted } from 'vue';

    const props = defineProps({
        goal: {
            type: Object,
            required: true
        }
    });

    const { goal } = toRefs(props);
    const today = new Date();
    const currentDate = ref(today.toISOString().split('T')[0]);
    const goalStartDateString = ref(new Date(goal.value.startDate).toDateString());
    const goalEndDateString = ref(new Date(goal.value.endDate).toDateString());
    const numberOfDaysLeft = ref(NaN);
    onMounted(() => {
        console.log("onMounted GoalListItem...");
        if (goal !== undefined) {
            const newValue = (new Date(goal.value.endDate) - new Date(currentDate.value)) / (24 * 60 * 60 * 1000);
            console.log("Days left:", newValue);
            numberOfDaysLeft.value = Math.ceil(newValue);
        }
    });

</script>

<template>
    <div>
        <header>
            {{ goalStartDateString }} - {{ goalEndDateString }}
        </header>
        <main>
            <b>{{ numberOfDaysLeft }}</b> days left to reach this goal...
        </main>
    </div>
</template>

<style scoped>

</style>
