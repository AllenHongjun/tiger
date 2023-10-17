import { ref } from 'vue'

export function useTasks() {
  const tasksRef = ref({
    key: '3',
    name: '待办',
    list: []
  })

  return {
    tasksRef
  }
}
