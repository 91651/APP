<template>
  <Card>
      <Table :columns="[
                    {
                        title: 'id',
                        key: 'id'
                    },
                    {
                        title: 'title',
                        key: 'title'
                    },
                    {
                        title: 'created',
                        key: 'created'
                    }
                ]" :data="articles"></Table>
    </Card>
</template>

<script lang="ts" src="./article.ts"></script>