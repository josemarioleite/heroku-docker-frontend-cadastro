<template>
    <q-select outlined :use-input="IsUseInput" :options="Options" :option-label="OptionsLabel" :option-value="OptionsValue">
        <template v-slot:prepend>
            <q-icon :name="Icon" color="black" />
        </template>
    </q-select>
</template>

<script>
export default {
  name: 'Select-Component',
  props: {
    Icon: String,
    IsUseInput: {
      default: true
    },
    Options: Array,
    OptionsLabel: String,
    OptionsValue: String
  }
}
</script>

<style>

</style>
