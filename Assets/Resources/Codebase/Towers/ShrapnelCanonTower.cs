﻿using Towers;
using UnityEngine;

public class ShrapnelCanonTower : AbstractTower
{
    [SerializeField]
    private int _shrapnelCount;
    [SerializeField]
    private int _shrapnelCountPerLevel;
    private void Awake()
    {
        InitializeBase();
        InitializeAttackModule<ShrapnelTowerAttakModule>();
        InitializeProjectileFactory(_towerStats.ProjectilePrefab);
        RefreshAttackDelay();
        UpdateAttackModule();
    }
    private void Update()
    {
        FinClosestTargetIfNeeded();
        CountAttackDelay();
        TryToAttack();
    }
    protected override void Attack()
    {
        _projectileFactory.ReturnProjectile(_towerStats.ProjectileSpeed, _currentEnemy.transform, transform.position, () => { TryDealDamageToCurrentEnemy(); });
        PlayAttackSound();
        RefreshAttackDelay();
    }

    protected override void InitializeProjectileFactory(ProjectileBehaviour projectilePrefab) => _projectileFactory = new SimpleProjectileFactory(projectilePrefab);
    protected override void Levelup()
    {
        base.Levelup();
        _shrapnelCount += _shrapnelCountPerLevel;
        UpdateAttackModule();
    }

    private void UpdateAttackModule()
    {
        ShrapnelTowerAttakModule module = (ShrapnelTowerAttakModule)_attackModule;
        module.ShrapnelCount = _shrapnelCount;
    }
}